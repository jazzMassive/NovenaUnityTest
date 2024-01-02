using System;
using System.Collections.Generic;

namespace Assets.Scripts.Model

{
    [Serializable]
    public class Topic
    {
        public string topicId;
        public string name;
        public string type;
        public List<Media> media;
    }
}