namespace NZwalks.API.Models
{
    public class TestClass
    {
        public int Id { get; set; }
        public string Running { get; set; }
        public int NrOfRuns { get; set; }
        public int KmRunned { get; set; }

        public TestClass()
        {

        }

        public TestClass(int id )
        {
            this.Id = id;

        }

        public TestClass(int id, string running, int nrofruns)
        {
            this.Id = id;
            this.Running = running;
            this.NrOfRuns = nrofruns;
        }

        public TestClass(int id, string running, int nrofruns, int kmrunned)
        {
            this.Id = id;
            this.Running = running;
            this.NrOfRuns = nrofruns;
            this.KmRunned = kmrunned;
            
        }
    }

    public class Execution 
    {
        //Intantiaza 3 clase cu cei 3 constructori // am mai adaugat un cunstructor.
        //Creeaza o interpolare cu clasele instantiate
        public void Calculate()
        {
            var test = new TestClass();
            var test1 = new TestClass(1);
            var test2 = new TestClass(2, "MountainRun", 20);
            var test3 = new TestClass(4, "LongBeach", 10, 120);

            if (test.KmRunned >= 100)
            {
                Console.WriteLine($"Alexandru ran {test3.Running} which has a length of {test3.KmRunned} KM");
            }

            else
            {
                Console.WriteLine($"Alexandru has to train more and run on {test2.Running} track which has a lenght of {test2.KmRunned} KM");
            }
        }

        
        

    }

    




}
