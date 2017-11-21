using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6_Pub
{
    class Patron
    {
        public string Name { get; set; }
        Queue<string> NameQueue = new Queue<string>();
        Queue<string> temporaryQueue = new Queue<string>();

        private int PatronDrinkTimeMin = 10000;
        private int PatronDrinkMax = 20000;

        public Patron(string name)
        {
            name = Name;
            NameQueue.Enqueue(Name);
        }
    }
}
