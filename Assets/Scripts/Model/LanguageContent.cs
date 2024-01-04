using System;
using System.Collections.Generic;

namespace Assets.Scripts.Model
{
    [Serializable]
    public class LanguageContent
    {
        public string LanguageId;
        public string LanguageName;
        public List<Topic> Topics;


        public Topic GetTopicById(string id)
        {
            return Topics.Find(u => u.TopicId == id);
        }
    }
}
