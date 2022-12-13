using Next_Level.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Next_Level.Classes
{
    public class FeedbackList
    {
        List<Feedback> feedbacks;
        IFile file;
        string feedbackPath;
        int count;
        public int Count 
        { 
            get {
                count = feedbacks.Count;
                return count;
            }  
        }

        public FeedbackList(string productName)
        {
            feedbackPath = createPath(productName);
            LoadComments();
        }

        string createPath(string productName)
        {
            string target = NextLevelPath.STOREBD_PATH;
            target = System.IO.Path.GetFullPath(target);
            target = System.IO.Path.Combine(target, productName);

            if (!Directory.Exists(target)) 
                Directory.CreateDirectory(target);

            target = System.IO.Path.Combine(target, productName + ".xml");
            return target;
        }

        public Feedback getFeedbackById(string id)
        {
            foreach(var feedback in feedbacks)
            {
                if (feedback.id == id)
                    return feedback;
            }
            return null;
        }

        public void AddNewComment(Feedback feedback)
        {
            feedback.id = "nl" + generateId();
            feedbacks.Add(feedback);
            SaveComments();
        }

        public void RemoveCommentById(Feedback feedback)
        {
            feedbacks.Remove(feedback);
            SaveComments();
        }

        string generateId()
        {
            string result = string.Empty;
            Random random = new Random();
            int[] id = new int[5];
            while (true)
            {
                result = string.Empty;
                for (int i = 0; i < 5; i++)
                {
                    id[i] = random.Next(0, 9);
                    result += id[i];
                }
                if (!idIsUnique(result))
                    break;
            }

            return result;
        }

        bool idIsUnique(string id)
        {
            foreach (var feedback in feedbacks)
            {
                if (feedback.id == id)
                    return true;
            }
            return false;
        }

        void SaveComments()
        {
            file = new XmlFormat(feedbackPath);
            file.Save<List<Feedback>>(feedbacks);
        }

        public void LoadComments()
        {
            if (File.Exists(feedbackPath))
            {
                file = new XmlFormat(feedbackPath);
                feedbacks = file.Load<List<Feedback>>();
            }
            else feedbacks = new List<Feedback>();
        }

        public IEnumerator<Feedback> GetEnumerator()
        {
            for (int i = 0; i < feedbacks.Count; i++)
                yield return feedbacks[i];
        }

    }
}
