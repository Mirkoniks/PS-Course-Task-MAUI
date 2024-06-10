//using DataLayer.DataBase;
//using DataLayer.Model;
//using System.Text;
//using Welcome.Model;
//using Welcome.Others;
//using static WelcomeExtended.Others.Delegates;

//try
//{
//    using (var context = new DatabaseContext()) 
//    {
//        context.Database.EnsureCreated();
//        chekUserFromDatabase(context);
//    }

//}
//catch (Exception e)
//{
//    var log = new ActionOnError(Log);
//    log += LogToFile;

//    //log(e.Message);
//}

//void chekUserFromDatabase(DatabaseContext databaseContext) 
//{
//    Console.WriteLine("Welcome to out School system!");
//    User user = new User();
//    while (true) 
//    {
//        Console.Write("Plese enter your name: ");
//        string namePromt = Console.ReadLine();

//        if (namePromt == "exit")
//        {
//            break;
//        }
//        Console.Write("Plese enter your password: ");
//        string passPromt = Console.ReadLine();

//        if (passPromt == "exit")
//        {
//            break;
//        }

//        var crypededPass = user.Encrypt(passPromt);
        
//        var result = databaseContext.Users.Where(u => u.Name == namePromt && u.Password == crypededPass).FirstOrDefault();
        
//        if (result != null)
//        {
//            Console.WriteLine("Valid user");
//            LogLoginSuccess(result.Name);
        
//            if (result.Role == UserRoleEnum.ADMIN)
//            {
//                Console.WriteLine($"Welcome Admin: {result.Name}");
//                showAdminMenu();
//            }
//            else
//            {
//                Console.WriteLine($"Welcome {result.Name}");
//            }
//        }
//        else
//        {
//            Console.WriteLine("Invalid usser");
//            LogLoginError(namePromt);
//        }
//    }
   
//}

//void showAdminMenu() 
//{
//    StringBuilder sb = new StringBuilder();

//    int option = 0;

//    bool isSelectedCommand = false;

//    while (true) 
//    {
//        try
//        {
//            sb.AppendLine("Chose option: ");
//            sb.AppendLine("1) Show All Users");
//            sb.AppendLine("2) Add new a user");
//            sb.AppendLine("3) Delete a user");
//            sb.AppendLine("4) Exit");

//            Console.Write(sb.ToString());
//            sb.Clear();
//            option = int.Parse(Console.ReadLine());

//            switch (option)
//            {
//                case 1:
//                    showAllUsers();
//                    isSelectedCommand = true;
//                    break;
//                case 2:
//                    createUser();
//                    isSelectedCommand = true;
//                    break;
//                case 3:
//                    deleteUser();
//                    isSelectedCommand = true;
//                    break;
//                case 4:
//                    return;
//                default:
//                    Console.WriteLine("There is no such command! Try again");
//                    break;
//            }

//            if (isSelectedCommand)
//            {
//                Console.Write(sb.ToString());

//                option = int.Parse(Console.ReadLine());
//            }

//        }
//        catch (Exception e)
//        {
//            LogToFile(e.Message);
//            Console.WriteLine("Invalid input! Try again\n");
//        }
//    }
//}

//void showAllUsers() 
//{
//    using (var context = new DatabaseContext())
//    {

//        var users = context.Users.ToList();

//        foreach (var item in users)
//        {
//            Console.WriteLine(item.ToString());
//        }
//    }
//}

//void createUser() 
//{
//    using (var context = new DatabaseContext())
//    {

//        Console.Write("Enter name: ");
//        string name = Console.ReadLine();

//        Console.Write("Enter password: ");
//        string pass = Console.ReadLine();

//        Console.Write("Enter role (ANONYMOUS, ADMIN, INSPECTOR, PROFESSOR, STUDENT): ");
//        UserRoleEnum role;
//        Enum.TryParse(Console.ReadLine(), true, out role);

//        Console.Write("Enter email: ");
//        string email = Console.ReadLine();

//        Console.Write("Enter student number: ");
//        string studentNumber = Console.ReadLine();

//        var user1 = new DatabaseUser()
//        {
//            Name = name,
//            Password = pass,
//            Role = role,
//            Expires = DateTime.Now.AddYears(10),
//            BirthDay = DateTime.UtcNow,
//            Email = email,
//            StudentNumber = studentNumber
//        };

//        context.Add<DatabaseUser>(user1);
//        context.SaveChanges();
//    }
//}

//void deleteUser() 
//{
//    Console.Write("Enter name: ");
//    string name = Console.ReadLine();

//    using (var context = new DatabaseContext())
//    {

//        var reuslt = context.Users.Where(u => u.Name == name).FirstOrDefault();

//        if (reuslt == null)
//        {
//            Console.WriteLine("There is no such user");
//        }
//        else 
//        {
//            context.Users.Remove(reuslt);
//        }

//        context.SaveChanges();
//    }
//}

//UserRoleEnum StringToRoleEnum(string roleString)
//{
//    if (Enum.TryParse(roleString.ToUpper(), true, out UserRoleEnum roleEnum))
//    {
//        return roleEnum;
//    }
//    else
//    {
//        throw new ArgumentException($"Invalid UserRoleEnum value: {roleString}");
//    }
//}