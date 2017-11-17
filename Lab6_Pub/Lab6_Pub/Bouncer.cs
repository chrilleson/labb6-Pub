using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6_Pub
{
    class Bouncer
    {
        string[] NameList = new string[]
        {
        "Liam",
        "Mason",
        "Jacob",
        "William",
        "Ethan",
        "James",
        "Alexander",
        "Michael",
        "Benjamin",
        "Elijah"
        };

        private string Random()
        {
            Random r = new Random();
            int summa = 0;
            for (int index = 0; index < NameList.Length; index++)
            {
                int nameList = r.Next(1, 10 + 1);
                NameList[index] = nameList.ToString();
                summa += nameList;
            }
            return summa.ToString();
        }

        Task bouncer = Task.Run(() =>
        {

        });
    }
}
