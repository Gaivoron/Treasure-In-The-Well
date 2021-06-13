using UnityEngine;

namespace Profiles
{
    public sealed class ProfileManager : IProfileManager
    {
        private const string ProfileSaveKey = "profile";

        private static IProfileManager _instance;

        public static IProfileManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ProfileManager();
                }

                return _instance;
            }
        }
#if UNITY_EDITOR
        //TODO - move to a different class.
        [UnityEditor.MenuItem("Saves/Clear", false, 0)]
        static void ClearSaves()
        {
            PlayerPrefs.DeleteAll();
        }
#endif
        public IProfile Profile
        {
            get;
            private set;
        }

        private ProfileManager()
        {
            Profile = PlayerPrefs.HasKey(ProfileSaveKey) ? JsonUtility.FromJson<Profile>(PlayerPrefs.GetString(ProfileSaveKey)) : new Profile();
            Profile.Changed += OnProfileChanged;
        }

        private void OnProfileChanged()
        {
            PlayerPrefs.SetString(ProfileSaveKey, JsonUtility.ToJson(Profile));
            PlayerPrefs.Save();
        }
    }

}