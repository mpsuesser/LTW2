using System.Collections.Generic;
using DataStructures.PriorityQueue;
using UnityEngine;

public class Ticker : SingletonBehaviour<Ticker> {
    private class PeriodicSubscriptionData {
        public IDoesThingsPeriodically PeriodicEvent { get; }
        
        private double InitialTimeOfSubscription { get; set; }
        private double PeriodicInterval { get; set; }
        public double TimeOfNextExecution { get; private set; }
        public bool FlaggedForRemoval { get; private set; }
        public bool FlaggedForReinsertion { get; private set; }

        public PeriodicSubscriptionData(IDoesThingsPeriodically periodicThing) {
            PeriodicEvent = periodicThing;

            InitializeTimeKeepingVariables();

            FlaggedForRemoval = false;
            FlaggedForReinsertion = false;
        }

        public void Executed() {
            double curTime = Time.time;
            double leftoverTime = curTime - TimeOfNextExecution;
            TimeOfNextExecution += PeriodicEvent.GetInterval() - leftoverTime;
        }

        public void Refresh() {
            InitializeTimeKeepingVariables();
            
            FlagForReinsertion();
        }

        private void InitializeTimeKeepingVariables() {
            InitialTimeOfSubscription = Time.time;
            TimeOfNextExecution = InitialTimeOfSubscription + PeriodicEvent.GetInterval();
        }

        public void FlagForRemoval() {
            FlaggedForRemoval = true;
        }

        private void FlagForReinsertion() {
            FlaggedForReinsertion = true;
        }

        public void UnflagForReinsertion() {
            FlaggedForReinsertion = false;
        }
    }
    
    private static PriorityQueue<PeriodicSubscriptionData, double> Subscriptions { get; set; }
    private static Dictionary<IDoesThingsPeriodically, PeriodicSubscriptionData> PeriodicEventToSubscriptionMap { get; set; }
    
    private void Awake() {
        InitializeSingleton(this);

        Subscriptions = new PriorityQueue<PeriodicSubscriptionData, double>(0);
        PeriodicEventToSubscriptionMap = new Dictionary<IDoesThingsPeriodically, PeriodicSubscriptionData>();
    }

    public static void Subscribe(IDoesThingsPeriodically periodicEvent) {
        PeriodicSubscriptionData subscription = new PeriodicSubscriptionData(periodicEvent);
        PeriodicEventToSubscriptionMap.Add(periodicEvent, subscription);
        EnqueueSubscription(subscription);
    }

    public static void RefreshSubscription(IDoesThingsPeriodically periodicEvent) {
        if (
            !PeriodicEventToSubscriptionMap.TryGetValue(
                periodicEvent,
                out PeriodicSubscriptionData subscription
            )
        ) {
            LTWLogger.LogError("Tried refreshing Ticker event but was not subscribed!");
            return;
        }

        subscription.Refresh();
    }

    public static void Unsubscribe(IDoesThingsPeriodically periodicEvent) {
        if (
            !PeriodicEventToSubscriptionMap.TryGetValue(
                periodicEvent,
                out PeriodicSubscriptionData subscription
            )
        ) {
            LTWLogger.LogError("Tried unsubscribing from Ticker but was not subscribed!");
            return;
        }

        subscription.FlagForRemoval();
    }
    
    private void Update() {
        float currentTime = Time.time;
        while (
            !Subscriptions.IsEmpty()
            && (
                Subscriptions.Top().TimeOfNextExecution < currentTime
                || Subscriptions.Top().FlaggedForRemoval
            )
        ) {
            PeriodicSubscriptionData subscription = Subscriptions.Pop();
            if (subscription.FlaggedForRemoval) {
                PeriodicEventToSubscriptionMap.Remove(subscription.PeriodicEvent);
                continue;
            }

            if (subscription.FlaggedForReinsertion) {
                subscription.UnflagForReinsertion();
                EnqueueSubscription(subscription);
                continue;
            }
            
            subscription.PeriodicEvent.DoPeriodicThing();
            subscription.Executed();
            EnqueueSubscription(subscription);
        }
    }

    private static void EnqueueSubscription(PeriodicSubscriptionData subscription) {
        Subscriptions.Insert(subscription, subscription.TimeOfNextExecution);
    }
}