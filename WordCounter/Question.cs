using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCounter
{
    public class Question
    {
        public string Text_i { get; set; }
        public string Name_Theory { get; set; }  
        public List<Answer> Answers { get; set; }
        public Question()
        {
        }
        public Question(string text, List<Answer> answers)
        {
            Text_i = text;
            Answers = answers;

        }
    }
}
