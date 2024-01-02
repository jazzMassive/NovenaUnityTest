using Assets.Scripts.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Model {

    [Serializable]
    public class AppContent
    {
        public List<LanguageContent> languageContents;


        public List<LanguageContent> GetAllLanguages()
        {
            return languageContents;
        }

        public LanguageContent GetLanguageById(string languageId)
        {
            return languageContents.Find(u => u.languageId == languageId);
        }


    }
}