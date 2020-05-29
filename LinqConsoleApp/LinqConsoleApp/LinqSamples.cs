﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqConsoleApp
{
    public class LinqSamples
    {
        public static IEnumerable<Emp> Emps { get; set; }
        public static IEnumerable<Dept> Depts { get; set; }

        public LinqSamples()
        {
            LoadData();
        }

        public void LoadData()
        {
            var empsCol = new List<Emp>();
            var deptsCol = new List<Dept>();

            #region Load depts
            var d1 = new Dept
            {
                Deptno = 1,
                Dname = "Research",
                Loc = "Warsaw"
            };

            var d2 = new Dept
            {
                Deptno = 2,
                Dname = "Human Resources",
                Loc = "New York"
            };

            var d3 = new Dept
            {
                Deptno = 3,
                Dname = "IT",
                Loc = "Los Angeles"
            };

            deptsCol.Add(d1);
            deptsCol.Add(d2);
            deptsCol.Add(d3);
            Depts = deptsCol;
            #endregion

            #region Load emps
            var e1 = new Emp
            {
                Deptno = 1,
                Empno = 1,
                Ename = "Jan Kowalski",
                HireDate = DateTime.Now.AddMonths(-5),
                Job = "Backend programmer",
                Mgr = null,
                Salary = 2000
            };

            var e2 = new Emp
            {
                Deptno = 1,
                Empno = 20,
                Ename = "Anna Malewska",
                HireDate = DateTime.Now.AddMonths(-7),
                Job = "Frontend programmer",
                Mgr = e1,
                Salary = 4000
            };

            var e3 = new Emp
            {
                Deptno = 1,
                Empno = 2,
                Ename = "Marcin Korewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Frontend programmer",
                Mgr = null,
                Salary = 5000
            };

            var e4 = new Emp
            {
                Deptno = 2,
                Empno = 3,
                Ename = "Paweł Latowski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Frontend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e5 = new Emp
            {
                Deptno = 2,
                Empno = 4,
                Ename = "Michał Kowalski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Backend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e6 = new Emp
            {
                Deptno = 2,
                Empno = 5,
                Ename = "Katarzyna Malewska",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Manager",
                Mgr = null,
                Salary = 8000
            };

            var e7 = new Emp
            {
                Deptno = null,
                Empno = 6,
                Ename = "Andrzej Kwiatkowski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "System administrator",
                Mgr = null,
                Salary = 7500
            };

            var e8 = new Emp
            {
                Deptno = 2,
                Empno = 7,
                Ename = "Marcin Polewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Mobile developer",
                Mgr = null,
                Salary = 4000
            };

            var e9 = new Emp
            {
                Deptno = 2,
                Empno = 8,
                Ename = "Władysław Torzewski",
                HireDate = DateTime.Now.AddMonths(-9),
                Job = "CTO",
                Mgr = null,
                Salary = 12000
            };

            var e10 = new Emp
            {
                Deptno = 2,
                Empno = 9,
                Ename = "Andrzej Dalewski",
                HireDate = DateTime.Now.AddMonths(-4),
                Job = "Database administrator",
                Mgr = null,
                Salary = 9000
            };

            empsCol.Add(e1);
            empsCol.Add(e2);
            empsCol.Add(e3);
            empsCol.Add(e4);
            empsCol.Add(e5);
            empsCol.Add(e6);
            empsCol.Add(e7);
            empsCol.Add(e8);
            empsCol.Add(e9);
            empsCol.Add(e10);
            Emps = empsCol;

            #endregion

        }


        
        public void example1()
        {
           
            var res = from emp in Emps
                      where emp.Job == "Backend programmer"
                      select new
                      {
                          Nazwisko = emp.Ename,
                          Zawod = emp.Job
                      };

            Print(res);
            
            var res2 = Emps.
                       Where(emp => emp.Job == "Backend programmer").
                       Select(emp => new
                       {
                           Nazwisko = emp.Ename,
                           Zawod = emp.Job
                       });

            Print(res2);
        }
        
        public void example2()
        {
            var answer = Emps
                      .Where((emp, indx) => emp.Job == "Frontend programmer" && emp.Salary > 1000)
                      .OrderByDescending(emp => emp.Ename);

            Print(answer);
        }
        
        public void example3()
        {
            var answer = Emps.Max(emp => emp.Salary);

            Print(answer);
        }
        
        public void example4()
        {
            var answer = Emps
                .Where(emp => {
                    return emp.Salary == Emps.Max(emp => emp.Salary);
                });
            
            Print(answer);
        }
        
        public void example5()
        {
            var answer = Emps
                .Select(emp => new {
                    Nazwisko = emp.Ename,
                    Praca = emp.Job
                });

            Print(answer);
        }
        
        public void example6()
        {
            var answer = Emps
                .Join(Depts,
                    emp => emp.Deptno,
                    dept => dept.Deptno,
                    (emp, dept) => new { Emp = emp, Dept = dept }
                );

            Print(answer);
        }
        
        public void example7()
        {
            var answer = Emps
                .GroupBy(emp => emp.Job)
                .Select(group => new
                {
                    Praca = group.Key,
                    LiczbaPracowników = group.Count()
                });

            Print(answer);
        }
        
        public void example8()
        {
            bool answer = Emps
                .Any(emp => emp.Job == "Backend programmer");

            Print(answer);
        }
        
        public void example9()
        {
            var answer = Emps
                .Where(emp => emp.Job == "Frontend programmer")
                .OrderByDescending(emp => emp.HireDate)
                .First();

            Print(answer);
        }
        
        public void example10()
        {
            var answer = Emps
                .Select(emp => new
                {
                    emp.Ename,
                    emp.Job,
                    emp.HireDate
                })
                .Union(new[] {
                    new
                    {
                        Ename = "Brak wartości",
                        Job = null as string,
                        HireDate = null as DateTime?
                    }
                });

            Print(answer);
        }
        
        public void example11()
        {
            var answer = Emps
                .Aggregate((a, b) => a.Salary < b.Salary ? b : a);

            Print(answer);
        }

        
        public void example12()
        {
            var answer = Emps
                .SelectMany(
                    dept => Depts,
                    (emp, dept) => new
                    {
                        Emp = emp,
                        Dept = dept
                    }
                );

            Print(answer);
        }

        private void Print<T>(IEnumerable<T> enumerable)
        {
            foreach (var e in enumerable) {
                Console.WriteLine(e);
            }

            Console.WriteLine("============================================================");
        }

        private void Print<T>(IOrderedEnumerable<T> orderedEnumerable) => Print(orderedEnumerable as IEnumerable<T>);

        private void Print<T>(T item)
        {
            Console.WriteLine(item);
            Console.WriteLine("============================================================");
        }
    }
}
