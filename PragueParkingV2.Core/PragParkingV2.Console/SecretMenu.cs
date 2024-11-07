using pragueParkingV2.Core.Models;
using PragueParkingV2.Core.Services;
using Spectre.Console;

namespace Prag_Parking_V2.PragParkingV2.Console
{
    public class SecretMenu
    {

        private readonly ParkingGarage _garage;
        private readonly string _password;

        public SecretMenu(ParkingGarage garage, string password)
        {
            _garage = garage;
            _password = password;
        }

        public void EnterSecretMenu()
        {
            // Frågar efter lösenordet
            var inputPassword = AnsiConsole.Ask<string>("Enter the secret password:");

            if (inputPassword != _password)
            {
                AnsiConsole.MarkupLine("[red]Incorrect password! Access denied.[/]");
                Thread.Sleep(1500);
                return;
            }

            // Om lösenordet är korrek så kommer man in
            ShowMenu();
        }

        private void ShowMenu()
        {
            AnsiConsole.Clear();

            AnsiConsole.MarkupLine("[bold red] ______     ______     ______     ______     ______     ______      __    __     ______     __   __     __  __    [/]");
            AnsiConsole.MarkupLine("[bold red]/\\  ___\\   /\\  ___\\   /\\  ___\\   /\\  == \\   /\\  ___\\   /\\__  _\\    /\\ \"-./  \\   /\\  ___\\   /\\ \"-.\\ \\   /\\ \\/\\ \\   [/]");
            AnsiConsole.MarkupLine("[bold red]\\ \\___  \\  \\ \\  __\\   \\ \\ \\____  \\ \\  __<   \\ \\  __\\   \\/_/\\ \\/    \\ \\ \\-./\\ \\  \\ \\  __\\   \\ \\ \\-.  \\  \\ \\ \\_\\ \\  [/]");
            AnsiConsole.MarkupLine("[bold red] \\/\\_____\\  \\ \\_____\\  \\ \\_____/  \\ \\_\\ \\_\\  \\ \\_____\\    \\ \\_\\     \\ \\_\\ \\ \\_\\  \\ \\_____\\  \\ \\ _\\\"\\_\\  \\ \\_____\\ [/]");
            AnsiConsole.MarkupLine("[bold red]  \\/_____/   \\/_____/   \\/_____/   \\/_/ /_/   \\/_____/     \\/_/      \\/_/  \\/_/   \\/_____/   \\/_/ \\/_/   \\/_____/  [/]");
            AnsiConsole.MarkupLine("[bold green]                                                                                                                    [/]");




            AnsiConsole.MarkupLine("[bold yellow]Welcome back Admin, what would you like to today?[/]");

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select an option:")
                    .PageSize(10) 
                    .AddChoices(new[] {
                "View all parked vehicles",
                "Remove all vehicles",
                "Exit secret menu"
                    }));

            switch (choice)
            {
                case "View all parked vehicles":
                    ViewParkedVehicles();
                    break;
                case "Remove all vehicles":
                    RemoveAllVehicles();
                    break;
                case "Exit secret menu":
                    AnsiConsole.MarkupLine("[yellow]Exiting secret menu...[/]");
                    break;
                default:
                    AnsiConsole.MarkupLine("[red]Invalid choice![/]");
                    break;
            }
        }


        private void RemoveAllVehicles()
        {
            var confirmation = AnsiConsole.Confirm("Are you sure you want to remove all vehicles?");
            if (confirmation)
            {
                _garage.RemoveAllVehicles();
                AnsiConsole.MarkupLine("[green]All vehicles have been removed.[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[yellow]Operation canceled.[/]");
            }
        }

        private void ViewParkedVehicles()
        {
            try
            {
                AnsiConsole.MarkupLine("[bold blue]Currently parked vehicles:[/]");

                // Skapa en tabell för att strukturera visningen
                var table = new Table();
                table.AddColumn("Spot ID");
                table.AddColumn("Status");
                table.AddColumn("License Plate");
                table.AddColumn("Parking Duration");
                table.AddColumn("Current Fee (CZK)");

                foreach (var spot in _garage.GetParkingSpots())
                {
                    if (spot is ParkingSpot parkingSpot)
                    {
                        // Om platsen är upptagen, visa registreringsnummer och parkerad tid
                        if (parkingSpot.IsOccupied)
                        {
                            TimeSpan duration = _garage.GetParkingDuration(parkingSpot.ParkedVehicle.LicensePlate);
                            int currentFee = _garage.CalculateParkingFee(parkingSpot.ParkedVehicle.LicensePlate); // Beräkna avgiften

                            table.AddRow(
                                parkingSpot.SpotId.ToString(),
                                "[red]Occupied[/]",
                                parkingSpot.ParkedVehicle.LicensePlate,
                                $"{duration.Hours}h {duration.Minutes}m",
                                $"{currentFee} CZK" // Visar aktuell avgift
                            );
                        }
                        else
                        {
                            // Om platsen är ledig, visa "Empty"
                            table.AddRow(
                                parkingSpot.SpotId.ToString(),
                                "[green]Empty[/]",
                                "-",
                                "-",
                                "-" // Ingen avgift för lediga platser
                            );
                        }
                    }
                }

                // Visa tabellen i konsolen
                AnsiConsole.Write(table);
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
            }

            // När alla fordon har visats
            AnsiConsole.MarkupLine("[yellow]Finished viewing parked vehicles. Press any key to return...[/]");
            AnsiConsole.Console.Input.ReadKey(false);
        }



       
    }
}

