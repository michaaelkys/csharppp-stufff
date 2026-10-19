using System;

// ============================================================
//  THE FIRST YEAR
//  A text adventure set during the first Harry Potter film.
//  You play an original character: a new first-year at Hogwarts.
//
//  Rules this program follows:
//   - Only functions, nested if/else, and switch cases
//   - No loops: each function calls the next story node
//   - Every story node offers 2 or more choices
//   - Hidden stats (never printed) decide the Sorting Hat result
//     and some outcomes later on
//
//  Console IO used:
//   Console.Write      -> print text, stay on the same line
//   Console.WriteLine  -> print text, then start a new line
//   Console.ReadLine   -> wait for the player to type + press Enter
//   Console.ForegroundColor / ResetColor -> text colour
// ============================================================

class Program
{
    // ---------- Hidden stats (never shown to the player) ----------
    static string playerName = "";
    static string house = "";
    static int courage = 0;
    static int wisdom = 0;
    static int loyalty = 0;
    static int ambition = 0;

    static void Main()
    {
        StartGame();
    }

    static void StartGame()
    {
        ResetState();
        Title();
        AskName();
        Train();
    }

    static void ResetState()
    {
        playerName = "";
        house = "";
        courage = 0;
        wisdom = 0;
        loyalty = 0;
        ambition = 0;
    }

    // ============================================================
    //  IO HELPERS
    // ============================================================

    // Read one line; quit cleanly if the input stream is closed.
    static string ReadInput()
    {
        string line = Console.ReadLine();
        if (line == null)
        {
            Environment.Exit(0);
        }
        return line.Trim();
    }

    static string Ask()
    {
        Console.WriteLine();
        Console.Write("> ");
        return ReadInput().ToLower();
    }

    static void Say(string text)
    {
        Console.WriteLine(text);
        Console.WriteLine();
    }

    static void Header(string title)
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("=== " + title + " ===");
        Console.ResetColor();
        Console.WriteLine();
    }

    static void Choice(string key, string text)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("  [" + key + "] ");
        Console.ResetColor();
        Console.WriteLine(text);
    }

    static void Invalid()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("That isn't one of the options. Type one of the numbers shown.");
        Console.ResetColor();
    }

    static void Pause()
    {
        Console.Write("(Press Enter to continue)");
        ReadInput();
    }

    static void Title()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("==========================================");
        Console.WriteLine("            THE FIRST YEAR");
        Console.WriteLine("==========================================");
        Console.ResetColor();
        Console.WriteLine();
        Say("Type the number of a choice and press Enter.");
    }

    static void AskName()
    {
        Console.Write("What is your name, first-year? ");
        string input = ReadInput();

        if (input == "")
        {
            Console.WriteLine("A name can't be blank.");
            AskName();
        }
        else
        {
            playerName = input;
        }
    }

    // ============================================================
    //  STORY
    // ============================================================

    // ---------- 1. The train (4 choices) ----------
    static void Train()
    {
        Header("The Hogwarts Express");
        Say("Your letter from Hogwarts came the same summer as Harry Potter's. " +
            "Now you're on the train, " + playerName + ", looking for a seat. " +
            "Every compartment is crowded.");

        Choice("1", "Sit with the boy with the scar and his red-haired friend");
        Choice("2", "Help a round-faced boy who has lost his toad");
        Choice("3", "Take a quiet corner seat and read your spellbook");
        Choice("4", "Sit near the pale boy who is bragging about his family");

        switch (Ask())
        {
            case "1":
                courage += 1;
                Sorting();
                break;
            case "2":
                loyalty += 1;
                Sorting();
                break;
            case "3":
                wisdom += 1;
                Sorting();
                break;
            case "4":
                ambition += 1;
                Sorting();
                break;
            default:
                Invalid();
                Train();
                break;
        }
    }

    // ---------- 2. The Sorting (3 questions, then the hat announces) ----------
    static void Sorting()
    {
        Header("The Sorting");
        Say("In the Great Hall, Professor McGonagall sets a frayed hat on a " +
            "stool and calls your name. The hat drops over your eyes, and a " +
            "voice only you can hear starts asking questions.");
        HatQ1();
    }

    static void HatQ1()
    {
        Say("\"What would you most like to be remembered for?\"");

        Choice("1", "Saving someone at great risk to myself");
        Choice("2", "Solving something nobody else could");
        Choice("3", "Being a friend who never gave up on anyone");
        Choice("4", "Achieving something everyone said was impossible");

        switch (Ask())
        {
            case "1":
                courage += 1;
                HatQ2();
                break;
            case "2":
                wisdom += 1;
                HatQ2();
                break;
            case "3":
                loyalty += 1;
                HatQ2();
                break;
            case "4":
                ambition += 1;
                HatQ2();
                break;
            default:
                Invalid();
                HatQ1();
                break;
        }
    }

    static void HatQ2()
    {
        Say("\"You find a locked door. What do you do?\"");

        Choice("1", "Open it first and ask questions later");
        Choice("2", "Figure out how the lock works");
        Choice("3", "Wait until my friends can come with me");
        Choice("4", "Find out who has the key, and what they want for it");

        switch (Ask())
        {
            case "1":
                courage += 1;
                HatQ3();
                break;
            case "2":
                wisdom += 1;
                HatQ3();
                break;
            case "3":
                loyalty += 1;
                HatQ3();
                break;
            case "4":
                ambition += 1;
                HatQ3();
                break;
            default:
                Invalid();
                HatQ2();
                break;
        }
    }

    static void HatQ3()
    {
        Say("\"And what do you fear most?\"");

        Choice("1", "Being a coward");
        Choice("2", "Not knowing the answer");
        Choice("3", "Being alone");
        Choice("4", "Being ordinary");

        switch (Ask())
        {
            case "1":
                courage += 1;
                Announce();
                break;
            case "2":
                wisdom += 1;
                Announce();
                break;
            case "3":
                loyalty += 1;
                Announce();
                break;
            case "4":
                ambition += 1;
                Announce();
                break;
            default:
                Invalid();
                HatQ3();
                break;
        }
    }

    // The hat picks the highest stat. Ties go in this order:
    // Gryffindor, Ravenclaw, Hufflepuff, Slytherin.
    static void Announce()
    {
        if (courage >= wisdom && courage >= loyalty && courage >= ambition)
        {
            house = "GRYFFINDOR";
        }
        else
        {
            if (wisdom >= loyalty && wisdom >= ambition)
            {
                house = "RAVENCLAW";
            }
            else
            {
                if (loyalty >= ambition)
                {
                    house = "HUFFLEPUFF";
                }
                else
                {
                    house = "SLYTHERIN";
                }
            }
        }

        Say("The hat is quiet for a long moment. Then it shouts to the whole hall:");

        switch (house)
        {
            case "GRYFFINDOR":
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case "RAVENCLAW":
                Console.ForegroundColor = ConsoleColor.Blue;
                break;
            case "HUFFLEPUFF":
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
            case "SLYTHERIN":
                Console.ForegroundColor = ConsoleColor.Green;
                break;
        }
        Console.WriteLine("        " + house + "!");
        Console.ResetColor();
        Console.WriteLine();

        Say("The " + house + " table cheers as you walk over and sit down.");
        Pause();
        Halloween();
    }

    // ---------- 3. Halloween (3 choices) ----------
    static void Halloween()
    {
        Header("Halloween Night");
        Say("Weeks later, the feast is interrupted when Professor Quirrell " +
            "runs in to say there's a troll in the dungeons, then faints. " +
            "Prefects start herding everyone to the dormitories. In the " +
            "crush, you overhear that Hermione Granger never came to the " +
            "feast. She's been crying in a girls' bathroom.");

        Choice("1", "Slip away and go warn her yourself");
        Choice("2", "Follow your prefect to the dormitory");
        Choice("3", "Tell a prefect that Hermione is missing");

        switch (Ask())
        {
            case "1":
                Troll();
                break;
            case "2":
                EndingSafe();
                break;
            case "3":
                loyalty += 1;
                Say("The prefect hurries off to find a professor, and you " +
                    "follow close behind. By the time you arrive, the " +
                    "teachers have already dealt with the troll and Hermione " +
                    "is safe.");
                ForbiddenCorridor();
                break;
            default:
                Invalid();
                Halloween();
                break;
        }
    }

    // ---------- 4. The troll (3 choices, nested if/else) ----------
    static void Troll()
    {
        Header("The Girls' Bathroom");
        Say("You reach the bathroom ahead of the teachers. A mountain troll " +
            "is smashing the sinks to pieces. Hermione is frozen against " +
            "the wall. Harry and Ron run in behind you.");

        Choice("1", "Cast a levitation charm on the troll's club");
        Choice("2", "Pull Hermione out of its way");
        Choice("3", "Shout and throw something to lure it toward you");

        switch (Ask())
        {
            case "1":
                if (wisdom >= 2)
                {
                    wisdom += 1;
                    Say("Your charm is perfect. The club floats out of the " +
                        "troll's hand and drops on its head.");
                    ForbiddenCorridor();
                }
                else
                {
                    if (courage >= 2)
                    {
                        Say("Your charm wobbles, but it's enough to twist " +
                            "the club out of the troll's grip. The teachers " +
                            "arrive just in time.");
                        ForbiddenCorridor();
                    }
                    else
                    {
                        EndingHospital("Your spell fizzles and the troll turns " +
                            "on you. The next thing you know, you're waking up " +
                            "in the hospital wing.");
                    }
                }
                break;
            case "2":
                loyalty += 1;
                Say("You grab Hermione's sleeve and drag her under a sink " +
                    "as the club smashes down. The teachers arrive moments " +
                    "later.");
                ForbiddenCorridor();
                break;
            case "3":
                courage += 1;
                if (courage >= 2)
                {
                    Say("The troll swings around to face you. You dodge " +
                        "just long enough for the teachers to burst in.");
                    ForbiddenCorridor();
                }
                else
                {
                    EndingHospital("You throw a shoe, miss, and the troll's " +
                        "club is faster than your legs. You wake up in the " +
                        "hospital wing.");
                }
                break;
            default:
                Invalid();
                Troll();
                break;
        }
    }

    // ---------- 5. The forbidden corridor (3 choices) ----------
    static void ForbiddenCorridor()
    {
        Header("The Third Floor");
        Say("Months later, you see Harry, Ron and Hermione sneaking toward " +
            "the forbidden third-floor corridor. You overhear why: something " +
            "valuable is hidden under a trapdoor there, guarded by a " +
            "three-headed dog, and they think someone is trying to steal it.");

        Choice("1", "Go with them");
        Choice("2", "Tell Professor Dumbledore");
        Choice("3", "Stay out of it");

        switch (Ask())
        {
            case "1":
                Snare();
                break;
            case "2":
                EndingTeacher();
                break;
            case "3":
                EndingStayedAway();
                break;
            default:
                Invalid();
                ForbiddenCorridor();
                break;
        }
    }

    // ---------- 6. The trapdoor (3 choices, nested if/else) ----------
    static void Snare()
    {
        Header("The Trapdoor");
        Say("You drop through the trapdoor and land on something soft and " +
            "damp. Vines wrap around your ankles and tighten. Hermione " +
            "gasps that the plant squeezes harder the more you fight it.");

        Choice("1", "Stay completely still and breathe");
        Choice("2", "Pull and twist to get free");
        Choice("3", "Cast a spell to make light");

        switch (Ask())
        {
            case "1":
                wisdom += 1;
                Say("The vines loosen and you slide down into the dark.");
                FinalChamber();
                break;
            case "2":
                EndingHospital("The vines squeeze tighter with every pull. " +
                    "You black out, and wake up in the hospital wing.");
                break;
            case "3":
                if (wisdom >= 2)
                {
                    Say("Your light bursts out, the vines recoil, and you " +
                        "drop free.");
                    FinalChamber();
                }
                else
                {
                    if (courage >= 3)
                    {
                        Say("You're terrified, but you push the spell out " +
                            "anyway. The light is just strong enough.");
                        FinalChamber();
                    }
                    else
                    {
                        EndingHospital("Your light flickers out and the vines " +
                            "close in. You wake up in the hospital wing.");
                    }
                }
                break;
            default:
                Invalid();
                Snare();
                break;
        }
    }

    // ---------- 7. The last chamber (3 choices, nested if/else) ----------
    static void FinalChamber()
    {
        Header("The Last Chamber");
        Say("In the last chamber, Professor Quirrell stands in front of an " +
            "old mirror. He isn't nervous anymore. A second face is on the " +
            "back of his head: Voldemort. The stone is hidden somewhere in " +
            "that mirror, and Harry is the only one who can get it.");

        Choice("1", "Stand beside Harry and keep Quirrell's attention on you");
        Choice("2", "Study the mirror to work out how it hides the stone");
        Choice("3", "Run back to find Professor Dumbledore");

        switch (Ask())
        {
            case "1":
                if (courage >= 3)
                {
                    EndingTriumph();
                }
                else
                {
                    if (loyalty >= 3)
                    {
                        EndingFriendship();
                    }
                    else
                    {
                        EndingHospital("Quirrell's hand closes around your " +
                            "shoulder and the world goes dark. You wake up in " +
                            "the hospital wing.");
                    }
                }
                break;
            case "2":
                if (wisdom >= 3)
                {
                    EndingTriumph();
                }
                else
                {
                    EndingHospital("You can't make sense of the mirror before " +
                        "Quirrell turns on you. You wake up in the hospital " +
                        "wing.");
                }
                break;
            case "3":
                EndingMessenger();
                break;
            default:
                Invalid();
                FinalChamber();
                break;
        }
    }

    // ============================================================
    //  ENDINGS
    // ============================================================

    static void Ending(string title, string text)
    {
        Header("ENDING: " + title);
        Say(text);
        PlayAgain();
    }

    static void EndingSafe()
    {
        Ending("Safe in the Common Room",
            "You get to your dormitory without any trouble. In the morning " +
            "you hear that Harry and Ron fought a troll in the girls' " +
            "bathroom, and you wonder what you'd have done in their place.");
    }

    static void EndingHospital(string cause)
    {
        Ending("The Hospital Wing",
            cause + " Madam Pomfrey tells you to rest. By the time you're " +
            "allowed to leave, the whole school is talking about what " +
            "happened.");
    }

    static void EndingTeacher()
    {
        Ending("A Word with the Headmaster",
            "Dumbledore listens carefully and thanks you. He says nothing " +
            "else. That night, the other three go ahead without you, and " +
            "you spend the whole time wondering how it turned out.");
    }

    static void EndingStayedAway()
    {
        Ending("An Ordinary Year",
            "You keep your head down and focus on homework. At the " +
            "end-of-year feast you hear what happened below the third " +
            "floor, and you feel a little smaller.");
    }

    static void EndingTriumph()
    {
        Ending("The Stone Stays Hidden",
            "Between you and Harry, Quirrell's attention is split just " +
            "long enough. Harry gets the stone first, and the dark spirit " +
            "leaves Quirrell for good. At the end-of-year feast, " +
            "Dumbledore calls out your name too.");
    }

    static void EndingFriendship()
    {
        Ending("A Friend to the End",
            "You don't stop Quirrell on your own, but you stay beside " +
            "Harry the whole way through. Months later, riding the " +
            "train home, you're sharing a compartment with the " +
            "three of them.");
    }

    static void EndingMessenger()
    {
        Ending("The Messenger",
            "You run back through the chambers as fast as you can. " +
            "Dumbledore is already on his way, and he squeezes your " +
            "shoulder as he passes. You catch your breath on the stairs.");
    }

    static void PlayAgain()
    {
        Console.Write("Play again? (y/n) ");

        switch (ReadInput().ToLower())
        {
            case "y":
            case "yes":
                StartGame();
                break;
            case "n":
            case "no":
                Say("Nox. Thanks for playing.");
                break;
            default:
                Invalid();
                PlayAgain();
                break;
        }
    }
}
