namespace Assignment2.Database.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Assignment2.Entities;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<Assignment2.Database.MyDatabase>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Assignment2.Database.MyDatabase context)
        {
            #region Seed Courses
            //==================== Seeding Courses ===========================================
            Course c1 = new Course() { Title = "Phisics", Stream = "Full time", Type = "Science" , StartDate = DateTime.Parse("2019/10/1"), EndDate = DateTime.Parse("2020/6/16") };
            Course c2 = new Course() { Title = "Chemistry", Stream = "Full time", Type = "Science" , StartDate = DateTime.Parse("2019/10/1"), EndDate = DateTime.Parse("2020/6/11") };
            Course c3 = new Course() { Title = "Biology", Stream = "Full time", Type = "Science" , StartDate = DateTime.Parse("2019/10/1"), EndDate = DateTime.Parse("2020/7/1") };
            Course c4 = new Course() { Title = "Mathematics", Stream = "Full time", Type = "Science" , StartDate = DateTime.Parse("2019/10/1"), EndDate = DateTime.Parse("2020/5/30") };
            Course c5 = new Course() { Title = "Information Technology", Stream = "Full time", Type = "Computer science" , StartDate = DateTime.Parse("2019/10/1"), EndDate = DateTime.Parse("2020/6/2") };
            #endregion


            #region Seed Trainers
            //==================== Seeding Trainers ===========================================
            Trainer t1 = new Trainer() { FirstName = "Albert", LastName = "Einstain", Subject = "Phisics", PhotoUrl= "../Content/Students_Image/boy4.png" };
            Trainer t2 = new Trainer() { FirstName = "Richard", LastName = "Faynman", Subject = "Phisics", PhotoUrl = "../Content/Students_Image/boy1.png" };
            Trainer t3 = new Trainer() { FirstName = "Marie", LastName = "Curie", Subject = "Chemistry", PhotoUrl = "../Content/Students_Image/girl4.png" };
            Trainer t4 = new Trainer() { FirstName = "Luis", LastName = "Pasteur", Subject = "Chemistry", PhotoUrl = "../Content/Students_Image/boy3.png" };
            Trainer t5 = new Trainer() { FirstName = "Francis", LastName = "Crick", Subject = "Biology", PhotoUrl = "../Content/Students_Image/boy2.png" };
            Trainer t6 = new Trainer() { FirstName = "James", LastName = "Watson", Subject = "Biology", PhotoUrl = "../Content/Students_Image/boy1.png" };
            Trainer t7 = new Trainer() { FirstName = "Leonhard", LastName = "Euler", Subject = "Mathematics", PhotoUrl = "../Content/Students_Image/boy3.png" };
            Trainer t8 = new Trainer() { FirstName = "Carl", LastName = "Gauss", Subject = "Mathematics", PhotoUrl = "../Content/Students_Image/boy4.png" };
            Trainer t9 = new Trainer() { FirstName = "Alan", LastName = "Turing", Subject = "Computer science", PhotoUrl = "../Content/Students_Image/boy1.png" };
            Trainer t10 = new Trainer() { FirstName = "Dennis", LastName = "Ritchie", Subject = "Computer science", PhotoUrl = "../Content/Students_Image/boy3.png" };
            #endregion


            #region Seed Assignmets
            //==================== Seeding Assignments ===========================================
            Assignment a1 = new Assignment() { Title = "PhysMec", Description = "Mechanics", SubDateTime= DateTime.Parse("2020/2/10") };
            Assignment a2 = new Assignment() { Title = "PhysOpt", Description = "Optics", SubDateTime = DateTime.Parse("2020/3/20") };
            Assignment a3 = new Assignment() { Title = "PhysWav", Description = "Waves", SubDateTime = DateTime.Parse("2020/4/15") };

            Assignment a4 = new Assignment() { Title = "ChemInorg1", Description = "Inorganic Chemistry I", SubDateTime = DateTime.Parse("2020/2/15"), Course = c2 };
            Assignment a5 = new Assignment() { Title = "ChemInorg2", Description = "Inorganic Chemistry II", SubDateTime = DateTime.Parse("2020/3/25"), Course = c2 };
            Assignment a6 = new Assignment() { Title = "ChemOrg", Description = "Organic Chemistry I", SubDateTime = DateTime.Parse("2020/4/15"), Course = c2 };

            Assignment a7 = new Assignment() { Title = "BioC", Description = "Cell Theory", SubDateTime = DateTime.Parse("2020/3/15"), Course = c3 };
            Assignment a8 = new Assignment() { Title = "BioEv", Description = "Evolution", SubDateTime = DateTime.Parse("2020/4/10"), Course = c3 };
            Assignment a9 = new Assignment() { Title = "BioGen", Description = "Gene theory", SubDateTime = DateTime.Parse("2020/5/30"), Course = c3 };

            Assignment a10 = new Assignment() { Title = "MathAl", Description = "Algebra", SubDateTime = DateTime.Parse("2020/3/12"), Course = c4 };
            Assignment a11 = new Assignment() { Title = "MathCal", Description = "Calculus", SubDateTime = DateTime.Parse("2020/4/25"), Course = c4 };
            Assignment a12 = new Assignment() { Title = "MathGT", Description = "Geometry and topology", SubDateTime = DateTime.Parse("2020/5/20"), Course = c4 };

            Assignment a13 = new Assignment() { Title = "ITAl", Description = "Algorithms", SubDateTime = DateTime.Parse("2020/2/20"), Course = c5 };
            Assignment a14 = new Assignment() { Title = "ITCD", Description = "Computer design", SubDateTime = DateTime.Parse("2020/3/26"), Course = c5 };
            Assignment a15 = new Assignment() { Title = "ITPM", Description = "Programming methodology", SubDateTime = DateTime.Parse("2020/5/25"), Course = c5 };
            #endregion


            #region Seed Students
            //==================== Seeding Students ===========================================
            Student s1 = new Student() { FirstName = "Maria", LastName = "Fafouti", DateOfBirth = DateTime.Parse("12/2/1993"), PhotoUrl = "../Content/Students_Image/girl4.png" };
            Student s2 = new Student() { FirstName = "Evangelos", LastName = "Koutsogiorgos", DateOfBirth = DateTime.Parse("10/12/1987"), PhotoUrl = "../Content/Students_Image/boy1.png" };
            Student s3 = new Student() { FirstName = "Panagiotis", LastName = "Koutsogiorgos", DateOfBirth = DateTime.Parse("22/2/1991"), PhotoUrl = "../Content/Students_Image/boy3.png" };
            Student s4 = new Student() { FirstName = "Ioannis", LastName = "Angelopoulos", DateOfBirth = DateTime.Parse("1/6/1985"), PhotoUrl = "../Content/Students_Image/boy2.png" };
            Student s5 = new Student() { FirstName = "Sophia", LastName = "Georgiou", DateOfBirth = DateTime.Parse("30/3/1993"), PhotoUrl = "../Content/Students_Image/girl3.png" };
            Student s6 = new Student() { FirstName = "Eleni", LastName = "Parisi", DateOfBirth = DateTime.Parse("20/8/1989"), PhotoUrl = "../Content/Students_Image/girl4.png" };

            Student s7 = new Student() { FirstName = "Athanasios", LastName = "Sdralias", DateOfBirth = DateTime.Parse("11/9/1986"), PhotoUrl = "../Content/Students_Image/boy1.png" };
            Student s8 = new Student() { FirstName = "Nikolaos", LastName = "Karageorgos", DateOfBirth = DateTime.Parse("5/11/1990"), PhotoUrl = "../Content/Students_Image/boy4.png" };
            Student s9 = new Student() { FirstName = "Margarita", LastName = "Fafouti", DateOfBirth = DateTime.Parse("2/2/1990"), PhotoUrl = "../Content/Students_Image/girl1.png" };
            Student s10 = new Student() { FirstName = "Panagiotis", LastName = "Sdralias", DateOfBirth = DateTime.Parse("12/7/1985"), PhotoUrl = "../Content/Students_Image/boy3.png" };
            Student s11 = new Student() { FirstName = "Anna", LastName = "Koutsogiorgou", DateOfBirth = DateTime.Parse("21/3/1987"), PhotoUrl = "../Content/Students_Image/girl2.png" };
            Student s12 = new Student() { FirstName = "Eirini", LastName = "Ntafou", DateOfBirth = DateTime.Parse("19/9/1984"), PhotoUrl = "../Content/Students_Image/girl1.png" };

            Student s13 = new Student() { FirstName = "Maria", LastName = "Dimitriou", DateOfBirth = DateTime.Parse("1/2/1991"), PhotoUrl = "../Content/Students_Image/girl3.png" };
            Student s14 = new Student() { FirstName = "Dimitrios", LastName = "Perikleous", DateOfBirth = DateTime.Parse("11/12/1990"), PhotoUrl = "../Content/Students_Image/boy4.png" };
            Student s15 = new Student() { FirstName = "Grigoris", LastName = "Arnaoutoglou", DateOfBirth = DateTime.Parse("13/6/1990"), PhotoUrl = "../Content/Students_Image/boy1.png" };
            Student s16 = new Student() { FirstName = "Dimitrios", LastName = "Gogos", DateOfBirth = DateTime.Parse("11/11/1992"), PhotoUrl = "../Content/Students_Image/boy3.png" };
            Student s17 = new Student() { FirstName = "Sotiris", LastName = "Apostolidis", DateOfBirth = DateTime.Parse("9/3/1984"), PhotoUrl = "../Content/Students_Image/boy1.png" };
            Student s18 = new Student() { FirstName = "Markos", LastName = "Seferlis", DateOfBirth = DateTime.Parse("24/2/1983"), PhotoUrl = "../Content/Students_Image/boy2.png" };

            Student s19 = new Student() { FirstName = "Vasiliki", LastName = "Kaminioti", DateOfBirth = DateTime.Parse("7/6/1987"), PhotoUrl = "../Content/Students_Image/girl2.png" };
            Student s20 = new Student() { FirstName = "Kleopatra", LastName = "Kotsovolou", DateOfBirth = DateTime.Parse("17/10/1991"), PhotoUrl = "../Content/Students_Image/girl1.png" };
            Student s21 = new Student() { FirstName = "Rayan", LastName = "Kelly", DateOfBirth = DateTime.Parse("27/7/1987"), PhotoUrl = "../Content/Students_Image/boy3.png" };
            Student s22 = new Student() { FirstName = "Panagiotis", LastName = "Zaxaris", DateOfBirth = DateTime.Parse("21/12/1986"), PhotoUrl = "../Content/Students_Image/boy4.png" };
            Student s23 = new Student() { FirstName = "Olga", LastName = "Kiritsopoulou", DateOfBirth = DateTime.Parse("24/4/1986"), PhotoUrl = "../Content/Students_Image/girl1.png" };
            Student s24 = new Student() { FirstName = "Xenofontas", LastName = "Vlaxogiannis", DateOfBirth = DateTime.Parse("18/8/1990"), PhotoUrl = "../Content/Students_Image/boy4.png" };

            Student s25 = new Student() { FirstName = "Hector", LastName = "Gatsos", DateOfBirth = DateTime.Parse("29/9/1987"), PhotoUrl = "../Content/Students_Image/boy3.png" };
            Student s26 = new Student() { FirstName = "Dimitra", LastName = "Alexiou", DateOfBirth = DateTime.Parse("16/8/1987"), PhotoUrl = "../Content/Students_Image/girl3.png" };
            Student s27 = new Student() { FirstName = "Olga", LastName = "Karageorgiou", DateOfBirth = DateTime.Parse("3/6/1992"), PhotoUrl = "../Content/Students_Image/girl4.png" };
            Student s28 = new Student() { FirstName = "Evangelos", LastName = "Mixail", DateOfBirth = DateTime.Parse("6/6/1985"), PhotoUrl = "../Content/Students_Image/boy3.png" };
            Student s29 = new Student() { FirstName = "Angeliki", LastName = "Karvouniari", DateOfBirth = DateTime.Parse("22/5/1989"), PhotoUrl = "../Content/Students_Image/girl1.png" };
            Student s30 = new Student() { FirstName = "Stephanos", LastName = "Adamos", DateOfBirth = DateTime.Parse("20/1/1986"), PhotoUrl = "../Content/Students_Image/boy2.png" };
            #endregion


            #region Seed StudentAssignment
            //==================== Seeding StudentAssignment ===========================================
            //StudentAssignment sa1 = new StudentAssignment() { Assignment = a1, Student = s1, OralMark = 80, TotalMark = 90 };
            //StudentAssignment sa2 = new StudentAssignment() { Assignment = a1, Student = s2, OralMark = 82, TotalMark = 94 };
            //StudentAssignment sa3 = new StudentAssignment() { Assignment = a2, Student = s3, OralMark = 70, TotalMark = 82 };
            //StudentAssignment sa4 = new StudentAssignment() { Assignment = a2, Student = s4, OralMark = 83, TotalMark = 80 };
            //StudentAssignment sa5 = new StudentAssignment() { Assignment = a3, Student = s5, OralMark = 70, TotalMark = 79 };
            //StudentAssignment sa6 = new StudentAssignment() { Assignment = a3, Student = s6, OralMark = 75, TotalMark = 76 };

            //StudentAssignment sa7 = new StudentAssignment() { Assignment = a4, Student = s7, OralMark = 84, TotalMark = 87 };
            //StudentAssignment sa8 = new StudentAssignment() { Assignment = a4, Student = s8, OralMark = 82, TotalMark = 82 };
            //StudentAssignment sa9 = new StudentAssignment() { Assignment = a5, Student = s9, OralMark = 81, TotalMark = 92 };
            //StudentAssignment sa10 = new StudentAssignment() { Assignment = a5, Student = s10, OralMark = 65, TotalMark = 75 };
            //StudentAssignment sa11 = new StudentAssignment() { Assignment = a6, Student = s11, OralMark = 70, TotalMark = 73 };
            //StudentAssignment sa12 = new StudentAssignment() { Assignment = a6, Student = s12, OralMark = 69, TotalMark = 74 };

            //StudentAssignment sa13 = new StudentAssignment() { Assignment = a7, Student = s13, OralMark = 78, TotalMark = 81 };
            //StudentAssignment sa14 = new StudentAssignment() { Assignment = a7, Student = s14, OralMark = 70, TotalMark = 72 };
            //StudentAssignment sa15 = new StudentAssignment() { Assignment = a8, Student = s15, OralMark = 74, TotalMark = 81 };
            //StudentAssignment sa16 = new StudentAssignment() { Assignment = a8, Student = s16, OralMark = 81, TotalMark = 79 };
            //StudentAssignment sa17 = new StudentAssignment() { Assignment = a9, Student = s17, OralMark = 82, TotalMark = 85 };
            //StudentAssignment sa18 = new StudentAssignment() { Assignment = a9, Student = s18, OralMark = 89, TotalMark = 78 };

            //StudentAssignment sa19 = new StudentAssignment() { Assignment = a10, Student = s19, OralMark = 92, TotalMark = 93 };
            //StudentAssignment sa20 = new StudentAssignment() { Assignment = a10, Student = s20, OralMark = 90, TotalMark = 82 };
            //StudentAssignment sa21 = new StudentAssignment() { Assignment = a11, Student = s21, OralMark = 75, TotalMark = 70 };
            //StudentAssignment sa22 = new StudentAssignment() { Assignment = a11, Student = s22, OralMark = 93, TotalMark = 80 };
            //StudentAssignment sa23 = new StudentAssignment() { Assignment = a12, Student = s23, OralMark = 86, TotalMark = 79 };
            //StudentAssignment sa24 = new StudentAssignment() { Assignment = a12, Student = s24, OralMark = 88, TotalMark = 81 };

            //StudentAssignment sa25 = new StudentAssignment() { Assignment = a13, Student = s25, OralMark = 80, TotalMark = 91 };
            //StudentAssignment sa26 = new StudentAssignment() { Assignment = a13, Student = s26, OralMark = 70, TotalMark = 70 };
            //StudentAssignment sa27 = new StudentAssignment() { Assignment = a14, Student = s27, OralMark = 88, TotalMark = 81 };
            //StudentAssignment sa28 = new StudentAssignment() { Assignment = a14, Student = s28, OralMark = 89, TotalMark = 87 };
            //StudentAssignment sa29 = new StudentAssignment() { Assignment = a15, Student = s29, OralMark = 75, TotalMark = 71 };
            //StudentAssignment sa30 = new StudentAssignment() { Assignment = a15, Student = s30, OralMark = 73, TotalMark = 69 };

            StudentAssignment sa1 = new StudentAssignment() { OralMark = 80, TotalMark = 90 };
            StudentAssignment sa2 = new StudentAssignment() { OralMark = 82, TotalMark = 94 };
            StudentAssignment sa3 = new StudentAssignment() { OralMark = 70, TotalMark = 82 };
            StudentAssignment sa4 = new StudentAssignment() { OralMark = 83, TotalMark = 80 };
            StudentAssignment sa5 = new StudentAssignment() { OralMark = 70, TotalMark = 79 };
            StudentAssignment sa6 = new StudentAssignment() { OralMark = 75, TotalMark = 76 };

            StudentAssignment sa7 = new StudentAssignment() { OralMark = 84, TotalMark = 87 };
            StudentAssignment sa8 = new StudentAssignment() { OralMark = 82, TotalMark = 82 };
            StudentAssignment sa9 = new StudentAssignment() { OralMark = 81, TotalMark = 92 };
            StudentAssignment sa10 = new StudentAssignment() { OralMark = 65, TotalMark = 75 };
            StudentAssignment sa11 = new StudentAssignment() { OralMark = 70, TotalMark = 73 };
            StudentAssignment sa12 = new StudentAssignment() { OralMark = 69, TotalMark = 74 };

            StudentAssignment sa13 = new StudentAssignment() { OralMark = 78, TotalMark = 81 };
            StudentAssignment sa14 = new StudentAssignment() { OralMark = 70, TotalMark = 72 };
            StudentAssignment sa15 = new StudentAssignment() { OralMark = 74, TotalMark = 81 };
            StudentAssignment sa16 = new StudentAssignment() { OralMark = 81, TotalMark = 79 };
            StudentAssignment sa17 = new StudentAssignment() { OralMark = 82, TotalMark = 85 };
            StudentAssignment sa18 = new StudentAssignment() { OralMark = 89, TotalMark = 78 };

            StudentAssignment sa19 = new StudentAssignment() { OralMark = 92, TotalMark = 93 };
            StudentAssignment sa20 = new StudentAssignment() { OralMark = 90, TotalMark = 82 };
            StudentAssignment sa21 = new StudentAssignment() { OralMark = 75, TotalMark = 70 };
            StudentAssignment sa22 = new StudentAssignment() { OralMark = 93, TotalMark = 80 };
            StudentAssignment sa23 = new StudentAssignment() { OralMark = 86, TotalMark = 79 };
            StudentAssignment sa24 = new StudentAssignment() { OralMark = 88, TotalMark = 81 };

            StudentAssignment sa25 = new StudentAssignment() { OralMark = 80, TotalMark = 91 };
            StudentAssignment sa26 = new StudentAssignment() { OralMark = 70, TotalMark = 70 };
            StudentAssignment sa27 = new StudentAssignment() { OralMark = 88, TotalMark = 81 };
            StudentAssignment sa28 = new StudentAssignment() { OralMark = 89, TotalMark = 87 };
            StudentAssignment sa29 = new StudentAssignment() { OralMark = 75, TotalMark = 71 };
            StudentAssignment sa30 = new StudentAssignment() { OralMark = 73, TotalMark = 69 };
            #endregion


            #region Seed StudentCourse
            //==================== Seeding StudentCourse ===========================================
            //StudentCourse sc1 = new StudentCourse() { Course = c1, Student = s1, TuitionFees = 500M };
            //StudentCourse sc2 = new StudentCourse() { Course = c1, Student = s2, TuitionFees = 500M };
            //StudentCourse sc3 = new StudentCourse() { Course = c1, Student = s3, TuitionFees = 450M };
            //StudentCourse sc4 = new StudentCourse() { Course = c1, Student = s4, TuitionFees = 500M };
            //StudentCourse sc5 = new StudentCourse() { Course = c1, Student = s5, TuitionFees = 500M };
            //StudentCourse sc6 = new StudentCourse() { Course = c1, Student = s6, TuitionFees = 500M };

            //StudentCourse sc7  = new StudentCourse() { Course = c2, Student = s7, TuitionFees = 500M };
            //StudentCourse sc8  = new StudentCourse() { Course = c2, Student = s8 , TuitionFees = 500M };
            //StudentCourse sc9  = new StudentCourse() { Course = c2, Student = s9 , TuitionFees = 450M };
            //StudentCourse sc10 = new StudentCourse() { Course = c2, Student = s10, TuitionFees = 500M };
            //StudentCourse sc11 = new StudentCourse() { Course = c2, Student = s11, TuitionFees = 450M };
            //StudentCourse sc12 = new StudentCourse() { Course = c2, Student = s12, TuitionFees = 500M };

            //StudentCourse sc13 = new StudentCourse() { Course = c3, Student = s13, TuitionFees = 500M };
            //StudentCourse sc14 = new StudentCourse() { Course = c3, Student = s14, TuitionFees = 500M };
            //StudentCourse sc15 = new StudentCourse() { Course = c3, Student = s15, TuitionFees = 500M };
            //StudentCourse sc16 = new StudentCourse() { Course = c3, Student = s16, TuitionFees = 500M };
            //StudentCourse sc17 = new StudentCourse() { Course = c3, Student = s17, TuitionFees = 500M };
            //StudentCourse sc18 = new StudentCourse() { Course = c3, Student = s18, TuitionFees = 500M };

            //StudentCourse sc19 = new StudentCourse() { Course = c4, Student = s19, TuitionFees = 400M };
            //StudentCourse sc20 = new StudentCourse() { Course = c4, Student = s20, TuitionFees = 500M };
            //StudentCourse sc21 = new StudentCourse() { Course = c4, Student = s21, TuitionFees = 500M };
            //StudentCourse sc22 = new StudentCourse() { Course = c4, Student = s22, TuitionFees = 450M };
            //StudentCourse sc23 = new StudentCourse() { Course = c4, Student = s23, TuitionFees = 500M };
            //StudentCourse sc24 = new StudentCourse() { Course = c4, Student = s24, TuitionFees = 500M };

            //StudentCourse sc25 = new StudentCourse() { Course = c5, Student = s25, TuitionFees = 500M };
            //StudentCourse sc26 = new StudentCourse() { Course = c5, Student = s26, TuitionFees = 400M };
            //StudentCourse sc27 = new StudentCourse() { Course = c5, Student = s27, TuitionFees = 500M };
            //StudentCourse sc28 = new StudentCourse() { Course = c5, Student = s28, TuitionFees = 500M };
            //StudentCourse sc29 = new StudentCourse() { Course = c5, Student = s29, TuitionFees = 450M };
            //StudentCourse sc30 = new StudentCourse() { Course = c5, Student = s30, TuitionFees = 500M };

            StudentCourse sc1 = new StudentCourse() { TuitionFees = 500 };
            StudentCourse sc2 = new StudentCourse() { TuitionFees = 500 };
            StudentCourse sc3 = new StudentCourse() { TuitionFees = 450 };
            StudentCourse sc4 = new StudentCourse() { TuitionFees = 500 };
            StudentCourse sc5 = new StudentCourse() { TuitionFees = 500 };
            StudentCourse sc6 = new StudentCourse() { TuitionFees = 500 };

            StudentCourse sc7 = new StudentCourse() {  TuitionFees = 500 };
            StudentCourse sc8 = new StudentCourse() {  TuitionFees = 500 };
            StudentCourse sc9 = new StudentCourse() {  TuitionFees = 450 };
            StudentCourse sc10 = new StudentCourse() { TuitionFees = 500 };
            StudentCourse sc11 = new StudentCourse() { TuitionFees = 450 };
            StudentCourse sc12 = new StudentCourse() { TuitionFees = 500 };

            StudentCourse sc13 = new StudentCourse() { TuitionFees = 500 };
            StudentCourse sc14 = new StudentCourse() { TuitionFees = 500 };
            StudentCourse sc15 = new StudentCourse() { TuitionFees = 500 };
            StudentCourse sc16 = new StudentCourse() { TuitionFees = 500 };
            StudentCourse sc17 = new StudentCourse() { TuitionFees = 500 };
            StudentCourse sc18 = new StudentCourse() { TuitionFees = 500 };

            StudentCourse sc19 = new StudentCourse() { TuitionFees = 400 };
            StudentCourse sc20 = new StudentCourse() { TuitionFees = 500 };
            StudentCourse sc21 = new StudentCourse() { TuitionFees = 500 };
            StudentCourse sc22 = new StudentCourse() { TuitionFees = 450 };
            StudentCourse sc23 = new StudentCourse() { TuitionFees = 500 };
            StudentCourse sc24 = new StudentCourse() { TuitionFees = 500 };

            StudentCourse sc25 = new StudentCourse() { TuitionFees = 500 };
            StudentCourse sc26 = new StudentCourse() { TuitionFees = 400 };
            StudentCourse sc27 = new StudentCourse() { TuitionFees = 500 };
            StudentCourse sc28 = new StudentCourse() { TuitionFees = 500 };
            StudentCourse sc29 = new StudentCourse() { TuitionFees = 450 };
            StudentCourse sc30 = new StudentCourse() { TuitionFees = 500 };
            #endregion


            #region Seed TrainerCourse
            TrainerCourse tc1 = new TrainerCourse();
            TrainerCourse tc2 = new TrainerCourse();
            TrainerCourse tc3 = new TrainerCourse();
            TrainerCourse tc4 = new TrainerCourse();
            TrainerCourse tc5 = new TrainerCourse();
            TrainerCourse tc6 = new TrainerCourse();
            TrainerCourse tc7 = new TrainerCourse();
            TrainerCourse tc8 = new TrainerCourse();
            TrainerCourse tc9 = new TrainerCourse();
            TrainerCourse tc10 = new TrainerCourse();
            #endregion


            #region Asign Assignments, Trainers and StudentCourses to Courses
            //=================== Asign Assignments, Trainers and StudentCourses to Courses ==========================
            c1.Assignments = new List<Assignment>() { a1, a2, a3 };
            c2.Assignments = new List<Assignment>() { a4, a5, a6 };
            c3.Assignments = new List<Assignment>() { a7, a8, a9 };
            c4.Assignments = new List<Assignment>() { a10, a11, a12 };
            c5.Assignments = new List<Assignment>() { a13, a14, a15 };

            c1.TrainerCourses = new List<TrainerCourse>() { tc1, tc2 };
            c2.TrainerCourses = new List<TrainerCourse>() { tc3, tc4 };
            c3.TrainerCourses = new List<TrainerCourse>() { tc5, tc6 };
            c4.TrainerCourses = new List<TrainerCourse>() { tc7, tc8 };
            c5.TrainerCourses = new List<TrainerCourse>() { tc9, tc10 };

            c1.StudentCourses = new List<StudentCourse> { sc1, sc2, sc3, sc4, sc5, sc6 };
            c2.StudentCourses = new List<StudentCourse> { sc7, sc8, sc9, sc10, sc11, sc12, };
            c3.StudentCourses = new List<StudentCourse> { sc13, sc14, sc15, sc16, sc17, sc18 };
            c4.StudentCourses = new List<StudentCourse> { sc19, sc20, sc21, sc22, sc23, sc24 };
            c5.StudentCourses = new List<StudentCourse> { sc25, sc26, sc27, sc28, sc29, sc30 };
            #endregion


            #region Asign StudentCourses and StudentAssignments to Students
            //=================== Asign StudentCourses and StudentAssignments to Students ==========================
            s1.StudentCourses = new List<StudentCourse> { sc1 };
            s2 .StudentCourses = new List<StudentCourse> { sc2 };
            s3 .StudentCourses = new List<StudentCourse> { sc3 };
            s4 .StudentCourses = new List<StudentCourse> { sc4 };
            s5 .StudentCourses = new List<StudentCourse> { sc5 };
            s6 .StudentCourses = new List<StudentCourse> { sc6 };
            s7 .StudentCourses = new List<StudentCourse> { sc7 };
            s8 .StudentCourses = new List<StudentCourse> { sc8 };
            s9 .StudentCourses = new List<StudentCourse> { sc9 };
            s10.StudentCourses = new List<StudentCourse> { sc10};
            s11.StudentCourses = new List<StudentCourse> { sc11};
            s12.StudentCourses = new List<StudentCourse> { sc12};
            s13.StudentCourses = new List<StudentCourse> { sc13};
            s14.StudentCourses = new List<StudentCourse> { sc14};
            s15.StudentCourses = new List<StudentCourse> { sc15};
            s16.StudentCourses = new List<StudentCourse> { sc16};
            s17.StudentCourses = new List<StudentCourse> { sc17};
            s18.StudentCourses = new List<StudentCourse> { sc18};
            s19.StudentCourses = new List<StudentCourse> { sc19};
            s20.StudentCourses = new List<StudentCourse> { sc20};
            s21.StudentCourses = new List<StudentCourse> { sc21};
            s22.StudentCourses = new List<StudentCourse> { sc22};
            s23.StudentCourses = new List<StudentCourse> { sc23};
            s24.StudentCourses = new List<StudentCourse> { sc24};
            s25.StudentCourses = new List<StudentCourse> { sc25};
            s26.StudentCourses = new List<StudentCourse> { sc26};
            s27.StudentCourses = new List<StudentCourse> { sc27};
            s28.StudentCourses = new List<StudentCourse> { sc28};
            s29.StudentCourses = new List<StudentCourse> { sc29};
            s30.StudentCourses = new List<StudentCourse> { sc30};
                                                           
            s1.StudentAssignments = new List<StudentAssignment> { sa1 };
            s2.StudentAssignments = new List<StudentAssignment> { sa2 };
            s3.StudentAssignments = new List<StudentAssignment> { sa3 };
            s4.StudentAssignments = new List<StudentAssignment> { sa4 };
            s5.StudentAssignments = new List<StudentAssignment> { sa5 };
            s6.StudentAssignments = new List<StudentAssignment> { sa6 };
            s7.StudentAssignments = new List<StudentAssignment> { sa7 };
            s8.StudentAssignments = new List<StudentAssignment> { sa8 };
            s9.StudentAssignments = new List<StudentAssignment> { sa9 };
            s10.StudentAssignments = new List<StudentAssignment> { sa10 };
            s11.StudentAssignments = new List<StudentAssignment> { sa11 };
            s12.StudentAssignments = new List<StudentAssignment> { sa12 };
            s13.StudentAssignments = new List<StudentAssignment> { sa13 };
            s14.StudentAssignments = new List<StudentAssignment> { sa14 };
            s15.StudentAssignments = new List<StudentAssignment> { sa15 };
            s16.StudentAssignments = new List<StudentAssignment> { sa16 };
            s17.StudentAssignments = new List<StudentAssignment> { sa17 };
            s18.StudentAssignments = new List<StudentAssignment> { sa18 };
            s19.StudentAssignments = new List<StudentAssignment> { sa19 };
            s20.StudentAssignments = new List<StudentAssignment> { sa20 };
            s21.StudentAssignments = new List<StudentAssignment> { sa21 };
            s22.StudentAssignments = new List<StudentAssignment> { sa22 };
            s23.StudentAssignments = new List<StudentAssignment> { sa23 };
            s24.StudentAssignments = new List<StudentAssignment> { sa24 };
            s25.StudentAssignments = new List<StudentAssignment> { sa25 };
            s26.StudentAssignments = new List<StudentAssignment> { sa26 };
            s27.StudentAssignments = new List<StudentAssignment> { sa27 };
            s28.StudentAssignments = new List<StudentAssignment> { sa28 };
            s29.StudentAssignments = new List<StudentAssignment> { sa29 };
            s30.StudentAssignments = new List<StudentAssignment> { sa30 };
            #endregion


            #region Asign StudentAssignments to Assignments
            //=================== Asign StudentAssignments to Assignments ==========================
            a1.StudentAssignments = new List<StudentAssignment>() {sa1, sa2 };
            a2.StudentAssignments = new List<StudentAssignment>() {sa3, sa4 };
            a3.StudentAssignments = new List<StudentAssignment>() {sa5, sa6 };
            a4.StudentAssignments = new List<StudentAssignment>() {sa7, sa8 };
            a5.StudentAssignments = new List<StudentAssignment>() {sa9, sa10 };
            a6.StudentAssignments = new List<StudentAssignment>() {sa11, sa12 };
            a7.StudentAssignments = new List<StudentAssignment>() {sa13, sa14 };
            a8.StudentAssignments = new List<StudentAssignment>() {sa15, sa16 };
            a9.StudentAssignments = new List<StudentAssignment>() { sa17, sa18 };
            a10.StudentAssignments = new List<StudentAssignment>() {sa19, sa20 };
            a11.StudentAssignments = new List<StudentAssignment>() {sa21, sa22 };
            a12.StudentAssignments = new List<StudentAssignment>() {sa23, sa24 };
            a13.StudentAssignments = new List<StudentAssignment>() {sa25, sa26 };
            a14.StudentAssignments = new List<StudentAssignment>() {sa27, sa28 };
            a15.StudentAssignments = new List<StudentAssignment>() {sa29, sa30 };
            #endregion

            #region Asign Trainers to TrainerCourse
            t1.TrainerCourses = new List<TrainerCourse>() { tc1 };
            t2.TrainerCourses = new List<TrainerCourse>() { tc2 };
            t3.TrainerCourses = new List<TrainerCourse>() { tc3 };
            t4.TrainerCourses = new List<TrainerCourse>() { tc4 };
            t5.TrainerCourses = new List<TrainerCourse>() { tc5 };
            t6.TrainerCourses = new List<TrainerCourse>() { tc6 };
            t7.TrainerCourses = new List<TrainerCourse>() { tc7 };
            t8.TrainerCourses = new List<TrainerCourse>() { tc8 };
            t9.TrainerCourses = new List<TrainerCourse>() { tc9 };
            t10.TrainerCourses = new List<TrainerCourse>() { tc10 };
            #endregion

            


            //=================== Insert data to the database ========================================
            context.Courses.AddOrUpdate(x => x.Title, c1, c2, c3, c4, c5);
            context.Trainers.AddOrUpdate(x => new { x.FirstName, x.LastName }, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10);
            context.Students.AddOrUpdate(x => new { x.FirstName, x.LastName }, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, s13, s14, s15, s16, s17, s18, s19, s20, s21, s22, s23, s24, s25, s26, s27, s28, s29, s30);


            context.SaveChanges();
        }
    }
}
