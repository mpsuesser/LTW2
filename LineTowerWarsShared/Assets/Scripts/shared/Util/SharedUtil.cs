public class SharedUtil {
    public static int ParseNumberFromString(string s) {
        string nums = "";
        foreach (char c in s) {
            if (c >= '0' && c <= '9') {
                nums += c;
            }
        }

        if (nums.Length == 0) {
            return 0;
        }

        return int.Parse(nums);
    }
}