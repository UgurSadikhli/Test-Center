using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCounter
{
    public class Answer
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }

        public Answer()
        {

        }
        public Answer(string text, bool isCorecct)
        {
            Text = text;
            IsCorrect = isCorecct;
        }
    }
}
