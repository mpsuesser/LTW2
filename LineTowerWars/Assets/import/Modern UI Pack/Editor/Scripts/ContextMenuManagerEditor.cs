﻿using UnityEngine;
using UnityEditor;

namespace Michsky.UI.ModernUIPack
{
    [CustomEditor(typeof(ContextMenuManager))]
    public class ContextMenuManagerEditor : Editor
    {
        private GUISkin customSkin;
        private ContextMenuManager cmTarget;
        private UIManagerContextMenu tempUIM;
        private int currentTab;

        private void OnEnable()
        {
            cmTarget = (ContextMenuManager)target;

            try { tempUIM = cmTarget.GetComponent<UIManagerContextMenu>(); }
            catch { }

            if (EditorGUIUtility.isProSkin == true) { customSkin = (GUISkin)Resources.Load("Editor\\MUI Skin Dark"); }
            else { customSkin = (GUISkin)Resources.Load("Editor\\MUI Skin Light"); }
        }

        public override void OnInspectorGUI()
        {
            MUIPEditorHandler.DrawComponentHeader(customSkin, "CM Top Header");

            GUIContent[] toolbarTabs = new GUIContent[3];
            toolbarTabs[0] = new GUIContent("Content");
            toolbarTabs[1] = new GUIContent("Resources");
            toolbarTabs[2] = new GUIContent("Settings");

            currentTab = MUIPEditorHandler.DrawTabs(currentTab, toolbarTabs, customSkin);

            if (GUILayout.Button(new GUIContent("Content", "Content"), customSkin.FindStyle("Tab Content")))
                currentTab = 0;
            if (GUILayout.Button(new GUIContent("Resources", "Resources"), customSkin.FindStyle("Tab Resources")))
                currentTab = 1;
            if (GUILayout.Button(new GUIContent("Settings", "Settings"), customSkin.FindStyle("Tab Settings")))
                currentTab = 2;

            GUILayout.EndHorizontal();

            var contextContent = serializedObject.FindProperty("contextContent");
            var contextAnimator = serializedObject.FindProperty("contextAnimator");
            var contextButton = serializedObject.FindProperty("contextButton");
            var contextSeparator = serializedObject.FindProperty("contextSeparator");
            var contextSubMenu = serializedObject.FindProperty("contextSubMenu");
            var autoSubMenuPosition = serializedObject.FindProperty("autoSubMenuPosition");
            var subMenuBehaviour = serializedObject.FindProperty("subMenuBehaviour");
            var vBorderTop = serializedObject.FindProperty("vBorderTop");
            var vBorderBottom = serializedObject.FindProperty("vBorderBottom");
            var hBorderLeft = serializedObject.FindProperty("hBorderLeft");
            var hBorderRight = serializedObject.FindProperty("hBorderRight");

            switch (currentTab)
            {
                case 0:
                    MUIPEditorHandler.DrawHeader(customSkin, "Content Header", 6);
                    MUIPEditorHandler.DrawProperty(vBorderTop, customSkin, "Vertical Top");
                    MUIPEditorHandler.DrawProperty(vBorderBottom, customSkin, "Vertical Bottom");
                    MUIPEditorHandler.DrawProperty(hBorderLeft, customSkin, "Horizontal Left");
                    MUIPEditorHandler.DrawProperty(hBorderRight, customSkin, "Horizontal Right");
                    break;

                case 1:
                    MUIPEditorHandler.DrawHeader(customSkin, "Core Header", 6);
                    MUIPEditorHandler.DrawProperty(contextContent, customSkin, "Context Content");
                    MUIPEditorHandler.DrawProperty(contextAnimator, customSkin, "Context Animator");
                    MUIPEditorHandler.DrawProperty(contextButton, customSkin, "Button Preset");
                    MUIPEditorHandler.DrawProperty(contextSeparator, customSkin, "Seperator Preset");
                    MUIPEditorHandler.DrawProperty(contextSubMenu, customSkin, "Sub Menu Preset");
                    break;

                case 2:
                    MUIPEditorHandler.DrawHeader(customSkin, "Options Header", 6);
                    autoSubMenuPosition.boolValue = MUIPEditorHandler.DrawToggle(autoSubMenuPosition.boolValue, customSkin, "Auto Sub Menu Position");
                    MUIPEditorHandler.DrawProperty(subMenuBehaviour, customSkin, "Sub Menu Behaviour");

                    MUIPEditorHandler.DrawHeader(customSkin, "UIM Header", 10);

                    if (tempUIM != null)
                    {
                        MUIPEditorHandler.DrawUIManagerConnectedHeader();

                        if (GUILayout.Button("Open UI Manager", customSkin.button))
                            EditorApplication.ExecuteMenuItem("Tools/Modern UI Pack/Show UI Manager");

                        if (GUILayout.Button("Disable UI Manager Connection", customSkin.button))
                        {
                            if (EditorUtility.DisplayDialog("Modern UI Pack", "Are you sure you want to disable UI Manager connection with the object? " +
                                "This operation cannot be undone.", "Yes", "Cancel"))
                            {
                                try { DestroyImmediate(tempUIM); }
                                catch { Debug.LogError("<b>[Context Menu]</b> Failed to delete UI Manager connection.", this); }
                            }
                        }
                    }

                    else if (tempUIM == null) { MUIPEditorHandler.DrawUIManagerDisconnectedHeader(); }

                    break;
            }

            this.Repaint();
            serializedObject.ApplyModifiedProperties();
        }
    }
}