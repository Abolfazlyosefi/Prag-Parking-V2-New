using PragueParkingV2.Core.Services;
using pragueParkingV2.Core.Models;
using Spectre.Console;
using Prag_Parking_V2.PragParkingV2.Console;


namespace pragueParkingV2.ConsoleApp
{
    public class DisplayManager
    {
        private ParkingGarage garage;
        private SecretMenu secretMenu;
        private ConfigurationManager configurationManager;

        public DisplayManager(ParkingGarage parkingGarage)
        {
            garage = parkingGarage;
            secretMenu = new SecretMenu(garage, "Sardinien");
            configurationManager = new ConfigurationManager();
        }
        
        public void ShowMainMenu()
        {
            ShowWelcomeAnimation();
            ShowPricingList();
            while (true)
            {
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("    __   __        __        ___     __        __               __  ");
                AnsiConsole.MarkupLine("   |__) |__)  /\\  / _` |  | |__     |__)  /\\  |__) |__/ | |\\ | / _` ");
                AnsiConsole.MarkupLine("   |    |  \\ /~~\\ \\__> \\__/ |___    |    /~~\\ |  \\ |  \\ | | \\| \\__> ");
                AnsiConsole.MarkupLine("                                                                      ");




                AnsiConsole.MarkupLine("");

                AnsiConsole.MarkupLine("[bold green]Welcome to Prague Parking![/]");

                
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("What would you like to do today?")
                        .AddChoices(new[] { "Park a vehicle", "Retrieve a vehicle", "Move a vehicle",  "Show parking garage status", "Reload pricing list", "Secret menu",  "Exit" })); // Lägg till menyval
                
                switch (choice)
                {
                    case "Park a vehicle":
                        ParkVehicle();
                        break;
                    case "Retrieve a vehicle":
                        RetrieveVehicle();
                        break;
                    case "Move a vehicle":
                        MoveVehicle();
                        break; 
                    case "Show parking garage status":
                        ShowParkingGarageStatus();
                        break;
                    case "Reload pricing list":
                        ReloadPricingConfig(); 
                        break;
                    case "Show pricing list": 
                        ShowPricingList(); 
                        break;
                    case "Secret menu":
                        secretMenu.EnterSecretMenu();
                        break;
                    case "Exit":
                        Exit();
                        break;
                }
                
            }
            
        }

        static void ShowWelcomeAnimation()
        {

            AnsiConsole.Clear();

            // Welcome animation
            var lines = new[]
{
    " ___    ____    ___                                        ___    ___",
    "  \\\\    //\\\\    //   II===   II        //===     ====      //\\\\  //\\\\      II===",
    "   \\\\  //  \\\\  //    II___   II       //       //    \\\\   //  \\\\//  \\\\     II___",
    "    \\\\//    \\\\//     II      II       \\\\       \\\\    //  //    --    \\\\    II   ",
    "     --      --      II===   II====    \\\\===     ====   //            \\\\   II===",
};

            // Skriv ut varje rad med kort fördröjning
            foreach (var line in lines)
            {
                AnsiConsole.MarkupLine($"[bold green]{line}[/]");
                Thread.Sleep(200);
            }


            // Kort paus efter "Welcome"
            AnsiConsole.MarkupLine("");
            AnsiConsole.MarkupLine("[bold blue]Welcome to Prague Parking![/]");
            Thread.Sleep(800);
            AnsiConsole.MarkupLine("");

            // Öppningsfraser visas en i taget
            AnsiConsole.MarkupLine("");
            AnsiConsole.Markup("                  [bold red]Secure Parking![/]");
            Thread.Sleep(800); // Kort paus
            AnsiConsole.MarkupLine("");

            AnsiConsole.MarkupLine(" ");
            AnsiConsole.Markup("                                      [bold blue]Cheap Prices![/]");
            Thread.Sleep(800); 
            AnsiConsole.MarkupLine("");

            AnsiConsole.MarkupLine("");
            AnsiConsole.Markup("                                                        [bold yellow]Best Service![/]");
            Thread.Sleep(1200); 

        }


        public void ShowParkingGarageStatus()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[bold blue]Parking Garage Status:[/]");

            // Anropa den nya metoden för att visa kartan
            garage.DisplayGarageMap();

            AnsiConsole.MarkupLine("[grey](Press any key to return)[/]");
            Console.ReadKey(true);
        }


        private void ParkVehicle()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[bold yellow]Park a Vehicle[/]");

            // Fråga användaren om fordonstyp
            var vehicleType = AnsiConsole.Ask<string>("Is it a Car or a Motorcycle? (C/M)").ToUpper(); // Konvertera till stora bokstäver
            if (vehicleType != "C" && vehicleType != "M")
            {
                AnsiConsole.MarkupLine("[red]Invalid vehicle type. Please enter C or M.[/]");
                Thread.Sleep(1200);
                return; // Avbryter om fordonstypen är ogiltig
            }

            // Fråga användaren om registreringsnummer
            var licensePlate = AnsiConsole.Ask<string>("Please enter your the vehicle's license plate:").ToUpper();

            // Kontrollera längden på registreringsnumret
            if (licensePlate.Length < 6 || licensePlate.Length > 10)
            {
                AnsiConsole.MarkupLine("[red]License plate must be between 6 and 10 characters.[/]");
                Thread.Sleep(1200);
                return; 
            }

            string vehicleId; // Deklarera ID för fordonet
            Vehicle vehicle; // Deklarera fordon
               
            // Skapa ID och fordon baserat på typen
            if (vehicleType == "C")
            {
                vehicleId = $"CAR#{licensePlate}"; // Skapa ID för bil
                vehicle = new Car(vehicleId); // Skapa Car med ID
            }
            else // Om fordonstyp är motorcykel
            {
                vehicleId = $"MC#{licensePlate}"; // Skapa ID för motorcykel
                vehicle = new Motorcycle(vehicleId); // Skapa Motorcycle med ID
            }

            // Animering för parkering
            for (int i = 10; i > 0; i--)
            {
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine($"{new string(' ', i)}🚗 [bold green]Parking in progress...[/]");
                Thread.Sleep(100);
            }

            // Försök att parkera fordonet
            if (garage.ParkVehicle(vehicle))
            {
                AnsiConsole.MarkupLine($"[green]Vehicle with ID {vehicleId} has been parked.[/]");
                Thread.Sleep(1200);
            }
            else
            {
                AnsiConsole.MarkupLine("[red]No available parking spots or vehicle already parked.[/]");
            }

            AnsiConsole.MarkupLine("[grey](Press any key to return)[/]");
            Console.ReadKey(true);


        }


        private void RetrieveVehicle()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[bold yellow]Retrieve a Vehicle[/]");

            // Fråga användaren om fordonstyp
            var vehicleType = AnsiConsole.Ask<string>("Is it a Car or a Motorcycle? (C/M)").ToUpper(); // Konvertera till stora bokstäver

            if (vehicleType != "C" && vehicleType != "M")
            {
                AnsiConsole.MarkupLine("[red]Invalid vehicle type. Please enter C or M.[/]");
                Thread.Sleep(1200);
                return; // Avbryt om fordonstypen är ogiltig    
            }

            // Fråga användaren om registreringsnummer (utan prefix)
            var licensePlate = AnsiConsole.Ask<string>("Enter the vehicle's license plate (ABC123):").ToUpper();

            // Generera ID baserat på fordonstyp
            string vehicleId = vehicleType == "C" ? $"CAR#{licensePlate}" : $"MC#{licensePlate}";

            for (int i = 0; i < 10; i++)
            {
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine($"{new string(' ', i)}🚗 [bold green]Retrieving vehicle in progress...[/]");
                Thread.Sleep(100);
            }

            // Deklarera parkingFee här
            int parkingFee = 0;

            // Kontrollera om fordonet finns och ta bort det
            if (garage.RemoveVehicle(vehicleId))
            {
                // Tilldela parkingFee efter att fordonet tagits bort
                parkingFee = garage.CalculateParkingFee(vehicleId);

                AnsiConsole.MarkupLine($"[green]Vehicle with ID {vehicleId} has been retrieved.[/]");
                AnsiConsole.MarkupLine($"[bold yellow]Total parking fee: {parkingFee} CZK[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Vehicle not found.[/]");
            }

            AnsiConsole.MarkupLine("[grey](Press any key to return)[/]");
            Console.ReadKey(true);
        }

        private void MoveVehicle()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[bold blue]Move Vehicle[/]");

            var licensePlate = AnsiConsole.Ask<string>("Enter the vehicle's license plate (ABC123):").ToUpper();

            // Fråga användaren om fordonstyp
            var vehicleType = AnsiConsole.Ask<string>("Is it a car or motorcycle? (Type 'car' or 'mc')").ToUpper();
            if (vehicleType != "CAR" && vehicleType != "MC")
            {
                AnsiConsole.MarkupLine("[red]Invalid vehicle type. Please enter 'car' or 'mc'.[/]");
                return;
            }

            var targetSpotId = AnsiConsole.Ask<int>("Please enter the parking spot number:");

            // Försök att flytta fordonet med hashtaggen
            bool success = garage.MoveVehicle($"{vehicleType}#{licensePlate}", vehicleType, targetSpotId);

            if (success)
            {
                AnsiConsole.MarkupLine($"[green]Vehicle {licensePlate} moved to parking spot {targetSpotId} successfully![/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Failed to move vehicle. Please check if the vehicle exists and the target spot is available.[/]");
            }

            AnsiConsole.MarkupLine("[grey](Press any key to return)[/]");
            Console.ReadKey(true);
        }



        private void Exit()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[bold red]Are you sure you want to exit?[/]");

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select an option:")
                    .AddChoices(new[] { "Yes", "No" }));

            if (choice == "Yes")
            {
                
                var goodbyeMessage = "Goodbye and drive safely! ✌️";


                foreach (var letter in goodbyeMessage)
                {
                    AnsiConsole.Markup($"[bold blue]{letter}[/]");
                    Thread.Sleep(50);
                }

                AnsiConsole.MarkupLine(""); // Ny rad efter meddelandet
                Thread.Sleep(800); 

                AnsiConsole.MarkupLine("[bold green]Thank you for using Prague Parking![/] 🚗");
                Thread.Sleep(1200); 

                Environment.Exit(0); // Avslutar programmet
            }
            else
            {
                AnsiConsole.MarkupLine("[yellow]Returning to the main menu...[/]");
            }
        }



        public void ReloadPricingConfig()
        {
            var newPricing = configurationManager.LoadPricingConfig();
            garage.UpdatePricing(newPricing);

            int carPrice = newPricing["Car"];
            int motorcyclePrice = newPricing["Motorcycle"];
            int freeMinutes = newPricing.ContainsKey("FreeMinutes") ? newPricing["FreeMinutes"] : 0;

            AnsiConsole.MarkupLine($"[green]Pricing list has been reloaded successfully![/]");
            AnsiConsole.MarkupLine($"[bold yellow]Updated Prices:[/]");
            AnsiConsole.MarkupLine($"Car: {carPrice} CZK per hour");
            AnsiConsole.MarkupLine($"Motorcycle: {motorcyclePrice} CZK per hour");
            AnsiConsole.MarkupLine($"First {freeMinutes} minutes are free of charge.");
            ShowPricingList();
        }

        private void ShowPricingList()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[bold blue]Pricing List:[/]");

            // Hämta prissättningen från garaget
            var pricing = garage.GetPricing(); 

            // Definiera en standardavgift för fria minuter
            if (pricing.TryGetValue("FreeMinutes", out int freeMinutes))
            {
                AnsiConsole.MarkupLine($"[bold yellow]Free Minutes:[/] {freeMinutes} minutes");
            }

            // Visa priser per fordonstyp
            foreach (var item in pricing)
            {
                if (item.Key != "FreeMinutes") // Exkludera fria minuter från denna loop
                {
                    AnsiConsole.MarkupLine($"[bold yellow]{item.Key}:[/] {item.Value} CZK per hour");
                }
            }

            AnsiConsole.MarkupLine("[grey](Press any key to return)[/]");
            Console.ReadKey(true);
        }


    }

}

  
                       
          