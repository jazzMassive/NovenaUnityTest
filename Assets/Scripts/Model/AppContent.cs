using Assets.Scripts.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Model {

    [Serializable]
    public class AppContent
    {
        public List<LanguageContent> LanguageContents;


        public List<LanguageContent> GetAllLanguages()
        {
            return LanguageContents;
        }

        public LanguageContent GetLanguageById(string languageId)
        {
            return LanguageContents.Find(u => u.LanguageId == languageId);
        }


    }
}