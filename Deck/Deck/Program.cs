using System;

namespace Deck
{
    class Program
    {
        static void Main(string[] args)
        {
            bool playing = true;

            //names and descriptions
            string[] allcards = { "Block", "Slash", "Heal", "Smash", "Cross Cut", "Deep Wounds", "Fire Ball", "Poison Dart", "Iron Skin", "Natural Healing", "Wrath", "Vampiric Touch" };
            string[] acdescription = { "Gain 4 shield", "Deal 5 points of damage", "Heal 5 points of damage", "Deal 11 points of damage", "Deal 4 points of damage twice", "Deal 8 points of damage and  maybe cause 1 bleed on hit", "Deal 10 points of damage and maybe cause 1 burning on hit", "Deal 1 point of damage and cause 5 poison on hit", "Gain 4 shield and gain 3 resistance", "Gain 5 regeneration", "Hit 4 times dealing 1 damage. Each hit increases the damage by 1. For each hit gain 1 strength", "Deal 11 damage and heal the damage caused" };
            string[] allitems = { "Elven Wooden Shield", "Small Refilling Potion", "Evolutionary Cell", "Dragon's Heart", "Snake Eyes", "SOwOrd", "Titan Milk" };
            string[] aidescription = { "Begin each turn with a shield equal to the number of units of this item", "Heal an amount equal to the number of units of this item after battle", "Start combat with 10% of crit chance. Each turn increase it by the number of units of this item", "Become immune to burning. NOT STACKABLE", "Become immune to poison. NOT STACKABLE", "Each attack deal an extra amount of damage equal to the number of units of this item", "Become immune to bleeding. NOT STACKABLE" };
            string[] statusfx = { "Burning", "Poison", "Bleeding", "Regeneration", "Strength", "Resistance" };
            string[] sedescription = { "Recieve an amount of damage equal to the amount of burning at the start of the turn, then decrease the amount of it by 1", "Recieve an amount of damage equal to the amount of poison at the start of the turn, then decrease the amount of it by 1", "Recieve an amount of damage equal to the amount of bleeding at the start of the turn, then decrease the amount of it by 1", "Heal an amount of damage equal to the amount of regeneration at the start of the turn, then decrease the amount of it by 1", "Increase the amount of damage by the amount of strength for the turn, then decrease the amount of it by 1", "Gain shield equal to the amount of resistance at the start of the turn, then decrease the amount of it by 1" };

            //Starting menu
            bool menu = true;
            while (menu == true)
            {
                Console.WriteLine("DECK\n\n\n1: Play\n\n2: Info\n\n3: Quit");
                string response = Console.ReadLine();
                switch (response)
                {
                    case "1":
                        playing = true;
                        menu = false;
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("\n---CARDS---");
                        for (int i = 0; i < allcards.Length; i++)
                        {

                            Console.WriteLine(allcards[i] + ": " + acdescription[i]);
                        }
                        Console.WriteLine("\n---ITEMS---");
                        for (int i = 0; i < allitems.Length; i++)
                        {

                            Console.WriteLine(allitems[i] + ": " + aidescription[i]);
                        }
                        Console.WriteLine("\n---STATUS EFFECTS---");
                        for (int i = 0; i < statusfx.Length; i++)
                        {

                            Console.WriteLine(statusfx[i] + ": " + sedescription[i]);
                        }
                        Console.WriteLine("Press anything to continue");
                        Console.ReadLine();
                        break;
                    case "3":
                        playing = false;
                        menu = false;
                        break;
                    default:
                        Console.WriteLine("Error booting up");
                        break;
                }
                Console.Clear();
            }

            //Playing
            while (playing == true)
            {
                
                

                int health = 40, bloc = 0, maxhealth, money = 0,startshield = 0,atkbonus = 0, startatkbonus = 0; 
                maxhealth = health;
                int critchance = 20;
                string[] deck = new string[40];
                string[] rooms = { "Enemy", "Shop", "Enemy", "Enemy","Enemy","Campfire","Chest" };
                //Immunities/Affected 0:Fire 1:Poison 2:Bleed
                int[] immunities = {0,0,0 };

                


                //The items are in order
                int[] items = new int[allitems.Length];
                

                //Starting cards

                for (int i =0; i <= 7; i++)
                {
                    deck[i] = "Slash";
                }
                for (int i = 8; i <= 15; i++)
                {
                    deck[i] = "Block";
                }
                for (int i = 16; i <= 19; i++)
                {
                    deck[i] = "Smash";
                }
                for (int i = 20; i <= 24; i++)
                {
                    deck[i] = "Heal";
                }
                for (int i = 25; i < deck.Length; i++)
                {
                    deck[i] = "null";
                }

                

                //Rooms 1 - 9

                for (int room = 1; room <= 9; room++)
                {
                    
                    Console.WriteLine("Room " + room);
                    Console.WriteLine("Money: " + money);

                    
                    string[] discard = new string[deck.Length];
                    string[] hand = new string[5];
                    string[] monsters = { "Zombie", "Jabali", "Skeleton", "Goblin", "Golem", "Slime", "Rat Swarm", "Scorched Soul" };
                    string[] bactionlist = { "attack", "block", "heal" };
                    string[] shopitems = new string[(allcards.Length + allitems.Length)];
                    //Adding shopitems
                    for(int i = 0; i < allcards.Length; i++)
                    {
                        shopitems[i] = allcards[i];
                    }
                    for(int i = allcards.Length; i < (allcards.Length + allitems.Length); i++)
                    {
                        shopitems[i] = allitems[(i-allcards.Length)];
                    }
                    

                    Random rng = new Random();
                    int bhealth = 1, bdam = 0, bbloc = 0, bshield = 0, card, maxbhealth, batknum = 0, bstartshield = 0, bheal = 0, price = 0 ,bcritchance = 0, crit =1, discardsize = 0, decksize = 0;
                    bool shopping = true;
                    string chamber1 = rooms[rng.Next(rooms.Length)], chamber2 = rooms[rng.Next(rooms.Length)], chamber = "null";
                    //Enemy immunities/Chance to status 0:Fire 1:Poison 2:Bleed 3:Regen 4:Strength 5:Resistance
                    int[] bimmunities = { 0, 0, 0 }, bstatchance = { 0, 0, 0 }, baffected = { 0, 0, 0 }, affected = { 0, 0, 0, 0, 0, 0 };

                    //Counting deck size

                    for (int i = 0; i < deck.Length; i++)
                    {
                        if(deck[i] != "null")
                        {
                            decksize++;
                        }
                    }

                    //Enemy Selection

                    string monster = monsters[rng.Next(monsters.Length)];
                    switch (monster)
                    {
                        case "Zombie":
                            bhealth = 16;
                            bdam = 4;
                            batknum = 1;
                            bbloc = 4;
                            bstartshield = 0;
                            bheal = 4;
                            bcritchance =10;
                            price = rng.Next(1, 5);
                            bimmunities[1] = 1;
                            break;
                        case "Jabali":
                            bhealth = 13;
                            bdam = 3;
                            batknum = 1;
                            bbloc = 6;
                            bstartshield = 2;
                            bheal = 1;
                            bcritchance =5;
                            price = rng.Next(5, 10);
                            break;
                        case "Skeleton":
                            bhealth = 12;
                            bdam = 6;
                            batknum = 1;
                            bbloc = 2;
                            bstartshield = 0;
                            bheal = 3;
                            bcritchance =20;
                            price = rng.Next(4, 7);
                            bimmunities[1] = 1;
                            bimmunities[2] = 1;
                            break;
                        case "Goblin":
                            bhealth = 10;
                            bdam = 2;
                            batknum = 3;
                            bbloc = 3;
                            bstartshield = 0;
                            bheal = 2;
                            bcritchance =15;
                            price = rng.Next(4,13);
                            bstatchance[2] = 10;
                            break;
                        case "Slime":
                            bhealth = 12;
                            bdam = 4;
                            batknum = 1;
                            bbloc = 2;
                            bstartshield = 2;
                            bheal = 5;
                            bcritchance =10;
                            price = rng.Next(2, 5);
                            bimmunities[1] = 1;
                            bstatchance[1] = 20;
                            break;
                        case "Golem":
                            bhealth = 20;
                            bdam = 4;
                            batknum = 1;
                            bbloc = 6;
                            bstartshield = 4;
                            bheal = 1;
                            bcritchance =5;
                            price = rng.Next(8,11);
                            bimmunities[2] = 1;
                            break;
                        case "Rat Swarm":
                            bhealth = 11;
                            bdam = 1;
                            batknum = 5;
                            bbloc = 2;
                            bstartshield = 0;
                            bheal = 2;
                            bcritchance = 20;
                            price = rng.Next(0, 6);
                            bstatchance[2] = 30;
                            bstatchance[1] = 50;
                            break;
                        case "Scorched Soul":
                            bhealth = 20;
                            bdam = 5;
                            batknum = 1;
                            bbloc = 2;
                            bstartshield = 0;
                            bheal = 1;
                            bcritchance = 10;
                            price = rng.Next(5, 13);
                            bstatchance[0] = 80;
                            baffected[0] = 1;
                            break;
                        default:
                            Console.WriteLine("Error selecting monster");
                            break;
                    }

                    maxbhealth = bhealth;
                    bshield = bstartshield;

                    //Selecting room
                    bool selecting = true;
                    while(selecting == true)
                    {
                        Console.WriteLine("There are 2 doors before you\n1: " + chamber1 + "\t 2: " + chamber2 + "\t 3:Info");
                        string chamberselected = Console.ReadLine();
                        switch (chamberselected)
                        {
                            case "1":
                                chamber = chamber1;
                                selecting = false;
                                break;
                            case "2":
                                chamber = chamber2;
                                selecting = false;
                                break;
                            case "3":
                                Console.Clear();
                                Console.WriteLine("\n---CARDS---");
                                for (int i = 0; i < allcards.Length; i++)
                                {

                                    Console.WriteLine(allcards[i] + ": " + acdescription[i]);
                                }
                                Console.WriteLine("\n---ITEMS---");
                                for (int i = 0; i < allitems.Length; i++)
                                {

                                    Console.WriteLine(allitems[i] + ": " + aidescription[i]);
                                }
                                Console.WriteLine("\n---STATUS EFFECTS---");
                                for (int i = 0; i < statusfx.Length; i++)
                                {

                                    Console.WriteLine(statusfx[i] + ": " + sedescription[i]);
                                }
                                Console.WriteLine("Press anything to continue");
                                Console.ReadLine();
                                break;
                            default:
                                Console.WriteLine("Error Selecting Chamber");
                                break;
                        }
                        Console.Clear();
                    }
                    



                    Console.WriteLine("Room " + room);
                    Console.WriteLine("Money: " + money);
                    //Chest
                    if (chamber == "Chest")
                    {
                        string chest1 = shopitems[rng.Next(shopitems.Length)], chest2 = shopitems[rng.Next(shopitems.Length)];
                        
                        
                            Console.WriteLine("You found a chest!!\n Pick one of its treasures");
                            Console.WriteLine("\n1: " + chest1 + "   2: " + chest2 + "   3: 8 coins");
                            string buy = Console.ReadLine();
                        switch (buy)
                        {
                            case "1":
                                for (int i = 0; i < allcards.Length; i++)
                                {
                                    if (allcards[i] == chest1 && decksize != deck.Length)
                                    {
                                        deck[decksize] = chest1;
                                        decksize++;
                                    }
                                    else if (decksize == deck.Length) Console.WriteLine("Deck is full");
                                }
                                for (int i = 0; i < allitems.Length; i++)
                                {
                                    if (allitems[i] == chest1)
                                    {
                                        items[i]++;
                                    }
                                }
                                break;
                            case "2":
                                for (int i = 0; i < allcards.Length; i++)
                                {
                                    if (allcards[i] == chest2 && decksize != deck.Length)
                                    {
                                        deck[decksize] = chest2;
                                        decksize++;
                                    }
                                    else if (decksize == deck.Length) Console.WriteLine("Deck is full");
                                }
                                for (int i = 0; i < allitems.Length; i++)
                                {
                                    if (allitems[i] == chest2)
                                    {
                                        items[i]++;
                                    }
                                }
                                break;
                            case "3":
                                money += 8;
                                break;
                            default:
                                Console.WriteLine("As you look for something that isn't there the chest dissapear");
                                break;
                        }
                    }

                    //Shop
                    if (chamber == "Shop")
                    {
                        string item1 = shopitems[rng.Next(shopitems.Length)], item2 = shopitems[rng.Next(shopitems.Length)], item3 = shopitems[rng.Next(shopitems.Length)];
                        while(shopping == true)
                        {
                            Console.WriteLine("Welcome to the shop!!");
                            Console.WriteLine("\n1: " + item1 + "(10)   2: " + item2 + "(10)   3: " + item3 + "(10)   4: Remove Card(8)   5: Exit");
                            string buy = Console.ReadLine();
                            switch (buy)
                            {
                                case "1":
                                    if (item1 != "null" && money >= 10)
                                    {
                                        for(int i = 0; i < allcards.Length; i++)
                                        {
                                            if(allcards[i] == item1 && decksize != deck.Length)
                                            {
                                                deck[decksize] = item1;
                                                decksize++;
                                            }
                                            else if (decksize == deck.Length) Console.WriteLine("Deck is full");
                                        }
                                        for (int i = 0; i < allitems.Length; i++)
                                        {
                                            if (allitems[i] == item1)
                                            {
                                                items[i]++;
                                            }
                                        }
                                        item1 = "null";
                                        money -= 10;
                                    }
                                    else if (money < 10) Console.WriteLine("Come when you have money");
                                    else Console.WriteLine("Out of stock");
                                    break;
                                case "2":
                                    if (item2 != "null" && money >= 10)
                                    {
                                        for (int i = 0; i < allcards.Length; i++)
                                        {
                                            if (allcards[i] == item2 && decksize != deck.Length)
                                            {
                                                deck[decksize] = item2;
                                                decksize++;
                                            }
                                            else if (decksize == deck.Length) Console.WriteLine("Deck is full");
                                        }
                                        for (int i = 0; i < allitems.Length; i++)
                                        {
                                            if (allitems[i] == item2)
                                            {
                                                items[i]++;
                                            }
                                        }
                                        item2 = "null";
                                        money -= 10;
                                    }
                                    else if (money < 10) Console.WriteLine("Come when you have money");
                                    else Console.WriteLine("Out of stock");
                                    break;
                                case "3":
                                    if (item3 != "null" && money >= 10)
                                    {
                                        for (int i = 0; i < allcards.Length; i++)
                                        {
                                            if (allcards[i] == item3 && decksize != deck.Length)
                                            {
                                                deck[decksize] = item3;
                                                decksize++;
                                            }
                                            else if (decksize == deck.Length) Console.WriteLine("Deck is full");
                                        }
                                        for (int i = 0; i < allitems.Length; i++)
                                        {
                                            if (allitems[i] == item3)
                                            {
                                                items[i]++;
                                            }
                                        }
                                        item3 = "null";
                                        money -= 10;
                                    }
                                    else if (money < 10) Console.WriteLine("Come when you have money");
                                    else Console.WriteLine("Out of stock");
                                    break;
                                case "4":
                                    if(money >= 8)
                                    {
                                        for (int i = 0; i < deck.Length; i++)
                                        {
                                            Console.WriteLine((i + 1) + ": " + deck[i]);
                                        }
                                        Console.WriteLine("\nWhat card do you want to delete?");
                                        deck[int.Parse(Console.ReadLine()) - 1] = "null";
                                        money -= 8;
                                    }
                                    else Console.WriteLine("Come when you have money");
                                    break;
                                case "5":
                                    shopping = false;
                                    break;
                                default:
                                        Console.WriteLine("Sorry, I don't have that");
                                        break;
                            }
                            Console.WriteLine("Money: " + money);
                        }
                    }

                    //Campfire
                    if(chamber == "Campfire")
                    {
                        health += (maxhealth / 20);
                        Console.WriteLine("You rest in the campfire\n1: Heal "+(maxhealth/20)+"\t 2: Add "+(maxhealth/10)+" to MaxHP");
                        string camp = Console.ReadLine();
                        switch (camp)
                        {
                            case "1":
                                health += (maxhealth / 20);
                                break;
                            case "2":
                                health += (maxhealth / 10);
                                maxhealth += (maxhealth / 10);
                                break;
                        }
                        if(health > maxhealth)
                        {
                            health = maxhealth;
                        }
                    }

                    // Draw 5 cards from deck

                    for (int i = 0; i < hand.Length; i++)
                    {
                        card = rng.Next(decksize);

                        if (deck[card] != "null")
                        {
                            hand[i] = deck[card];
                            deck[card] = "null";
                        }
                        else
                        {
                            i--;
                        }
                    }


                    //Fight

                    while (health > 0 && bhealth > 0 && chamber == "Enemy")
                    {


                        atkbonus = startatkbonus;
                        bloc = startshield;
                        //Status Effects

                        //Burning
                        if (immunities[0] == 0 && affected[0] > 0 )
                        {
                            Console.WriteLine("You are burning!!\nYou take " + affected[0] + " points of damage");
                            health -= affected[0];
                            affected[0]--;
                            
                        }
                        //Poisoned
                        if (immunities[1] == 0 && affected[1] > 0)
                        {
                            Console.WriteLine("You are poisoned!!\nYou take " + affected[1] + " points of damage");
                            health -= affected[1];
                            affected[1]--;
                        }
                        //Bleeding
                        if (immunities[2] == 0 && affected[2] > 0)
                        {
                            Console.WriteLine("You are bleeding!!\nYou take " + affected[2] + " points of damage");
                            health -= affected[2];
                            affected[2]--;
                        }
                        //Regen
                        if (affected[3] > 0)
                        {
                            Console.WriteLine("Your wounds are closing!!\nYou heal " + affected[3] + " points of damage");
                            health += affected[3];
                            affected[3]--;
                        }
                        //Strength
                        if (affected[4] > 0)
                        {
                            Console.WriteLine("Your muscles become stronger!!\nYour attacks dealed this round " + affected[4] + " extra points of damage");
                            atkbonus += affected[4];
                            affected[4]--;
                        }
                        //Resistance
                        if (affected[5] > 0)
                        {
                            Console.WriteLine("Your skin becomes thicker!!\nYour shield is " + affected[5] + " extra points of damage higher this round");
                            bloc += affected[5];
                            affected[5]--;
                        }


                        string baction = bactionlist[rng.Next(3)];
                        Console.WriteLine("\nYou:\nHp: " + health + "\nShield: " + bloc +"\n \n" + monster + "\nHp: " + bhealth + "\nShield: " + bshield + "\n" + monster +" is going to " + baction);
                        Console.WriteLine("\nYour hand is 1:" + hand[0] + " 2:" + hand[1] + " 3:" + hand[2] + " 4:" + hand[3] + " 5:" + hand[4]);
                        Console.WriteLine("\nYour turn\nPlease select a card to play");
                        string action = Console.ReadLine();
                        bool repeat = false;
                        Console.WriteLine();
                        //Player turn

                        switch (action)
                        {
                            case "1":
                            case "2":
                            case "3":
                            case "4":
                            case "5":
                                string play = hand[int.Parse(action) - 1];
                                switch (play)
                                {
                                    case "Heal":
                                        health += 5;
                                        Console.WriteLine("You heal " + 5 + " points of damage");
                                        if (health > maxhealth)
                                        {
                                            health = maxhealth;
                                        }
                                        break;
                                    case "Slash":

                                        int atk = 5 ,critroll = rng.Next(1,101);
                                        if (critroll <= critchance)
                                        { 
                                            crit = 2;
                                            Console.WriteLine("Crit!!");
                                        }
                                        else crit = 1;
                                        Console.WriteLine("You deal " + ((atk + atkbonus) * crit) + " points of damage");
                                        if (bshield >= (atk + atkbonus) * crit)
                                        {
                                            Console.WriteLine(monster +" blocked the attack completely!!");
                                        }
                                        else
                                        {
                                            bhealth -= ((atk + atkbonus) * crit - bshield);
                                        }
                                        bshield -= ((atk + atkbonus) * crit);
                                        crit = 1;
                                        break;
                                    case "Cross Cut":

                                        for(int i = 1; i <=2;i++)
                                        {
                                            int atk3 = 4, critroll3 = rng.Next(1, 101);
                                            if (critroll3 <= critchance)
                                            {
                                                crit = 2;
                                                Console.WriteLine("Crit!!");
                                            }
                                            else crit = 1;
                                            Console.WriteLine("You deal " + ((atk3 + atkbonus) * crit) + " points of damage");
                                            if (bshield >= (atk3 + atkbonus) * crit)
                                            {
                                                Console.WriteLine(monster + " blocked the attack completely!!");
                                            }
                                            else
                                            {
                                                bhealth -= ((atk3 + atkbonus) * crit - bshield);
                                            }
                                            bshield -= ((atk3 + atkbonus) * crit);
                                            crit = 1;
                                        }
                                        break;
                                    case "Block":
                                        bloc += 4;
                                        Console.WriteLine("You shielded for " + bloc);
                                        break;
                                    case "Smash":
                                        
                                        int atk2 = 11, critroll2 = rng.Next(1, 101);
                                        if (critroll2 <= critchance)
                                        {
                                            crit = 2;
                                            Console.WriteLine("Crit!!");
                                        }
                                        else crit = 1;
                                        Console.WriteLine("You deal " + ((atk2 + atkbonus) * crit) + " points of damage");
                                        if (bshield >= (atk2 + atkbonus) * crit)
                                        {
                                            Console.WriteLine(monster +" blocked the attack completely!!");
                                        }
                                        else
                                        {
                                            bhealth -= ((atk2 + atkbonus) * crit - bshield);
                                        }
                                        bshield -= ((atk2 + atkbonus) * crit);
                                        crit = 1;
                                        break;
                                    case "Deep Wounds":

                                        int atk4 = 8, critroll4 = rng.Next(1, 101);
                                        if (critroll4 <= critchance)
                                        {
                                            crit = 2;
                                            Console.WriteLine("Crit!!");
                                        }
                                        else crit = 1;
                                        Console.WriteLine("You deal " + ((atk4 + atkbonus) * crit) + " points of damage");
                                        if (bshield >= (atk4 + atkbonus) * crit)
                                        {
                                            Console.WriteLine(monster + " blocked the attack completely!!");
                                        }
                                        else
                                        {
                                            bhealth -= ((atk4 + atkbonus) * crit - bshield);
                                            if(30 >= rng.Next(1, 101))
                                            {
                                                baffected[2] = 1;
                                                Console.WriteLine(monster + " is bleeding");
                                            }
                                        }
                                        bshield -= ((atk4 + atkbonus) * crit);
                                        crit = 1;
                                        break;
                                    case "Fire Ball":

                                        int atk5 = 10, critroll5 = rng.Next(1, 101);
                                        if (critroll5 <= critchance)
                                        {
                                            crit = 2;
                                            Console.WriteLine("Crit!!");
                                        }
                                        else crit = 1;
                                        Console.WriteLine("You deal " + ((atk5 + atkbonus) * crit) + " points of damage");
                                        if (bshield >= (atk5 + atkbonus) * crit)
                                        {
                                            Console.WriteLine(monster + " blocked the attack completely!!");
                                        }
                                        else
                                        {
                                            bhealth -= ((atk5 + atkbonus) * crit - bshield);
                                            if (20 >= rng.Next(1, 101))
                                            {
                                                baffected[0] = 1;
                                                Console.WriteLine(monster + " is burning");
                                            }
                                        }
                                        bshield -= ((atk5 + atkbonus) * crit);
                                        crit = 1;
                                        break;
                                    case "Poison Dart":

                                        int atk6 = 1, critroll6 = rng.Next(1, 101);
                                        if (critroll6 <= critchance)
                                        {
                                            crit = 2;
                                            Console.WriteLine("Crit!!");
                                        }
                                        else crit = 1;
                                        Console.WriteLine("You deal " + ((atk6 + atkbonus) * crit) + " points of damage");
                                        if (bshield >= (atk6 + atkbonus) * crit)
                                        {
                                            Console.WriteLine(monster + " blocked the attack completely!!");
                                        }
                                        else
                                        {
                                            bhealth -= ((atk6 + atkbonus) * crit - bshield);
                                            if (100 >= rng.Next(1, 101))
                                            {
                                                baffected[1] = 5;
                                                Console.WriteLine(monster + " is poisoned");
                                            }
                                        }
                                        bshield -= ((atk6 + atkbonus) * crit);
                                        crit = 1;
                                        break;
                                    case "Iron Skin":
                                        affected[5] = 3;
                                        bloc += 4;
                                        Console.WriteLine("You have resistance");
                                        Console.WriteLine("You shielded for " + bloc);
                                        break;
                                    case "Natural Healing":
                                        affected[3] = 5;
                                        Console.WriteLine("You have regeneration");
                                        break;
                                    case "Wrath":
                                        for (int i = 0; i <= 4; i++)
                                        {
                                            atkbonus++;
                                            int atk7 = 1, critroll7 = rng.Next(1, 101);
                                            if (critroll7 <= critchance)
                                            {
                                                crit = 2;
                                                Console.WriteLine("Crit!!");
                                            }
                                            else crit = 1;
                                            Console.WriteLine("You deal " + ((atk7 + atkbonus) * crit) + " points of damage");
                                            if (bshield >= (atk7 + atkbonus) * crit)
                                            {
                                                Console.WriteLine(monster + " blocked the attack completely!!");
                                            }
                                            else
                                            {
                                                bhealth -= ((atk7 + atkbonus) * crit - bshield);
                                                affected[4]++;
                                            }
                                            bshield -= ((atk7 + atkbonus) * crit);
                                            crit = 1;
                                        }
                                        break;
                                    case "Vampiric Touch":
                                        int atk8 = 11, critroll8 = rng.Next(1, 101);
                                        if (critroll8 <= critchance)
                                        {
                                            crit = 2;
                                            Console.WriteLine("Crit!!");
                                        }
                                        else crit = 1;
                                        Console.WriteLine("You deal " + ((atk8 + atkbonus) * crit) + " points of damage");
                                        if (bshield >= (atk8 + atkbonus) * crit)
                                        {
                                            Console.WriteLine(monster + " blocked the attack completely!!");
                                        }
                                        else
                                        {
                                            bhealth -= ((atk8 + atkbonus) * crit - bshield);
                                            health +=((atk8 + atkbonus) * crit - bshield);
                                        }
                                        bshield -= ((atk8 + atkbonus) * crit);
                                        crit = 1;
                                        if (health > maxhealth)
                                        {
                                            health = maxhealth;
                                        }
                                        break;

                                    default:
                                        Console.WriteLine("Error in player cards");
                                        break;

                                }

                                card = rng.Next(decksize);
                                while (deck[card] == "null")
                                {
                                    card = rng.Next(decksize);
                                }

                                hand[int.Parse(action) - 1] = deck[card];
                                deck[card] = "null";
                                discard[discardsize] = play;
                                discardsize++;

                                break;

                            default:
                                Console.WriteLine("Please select a valid action");
                                repeat = true;
                                break;


                        }

                        //Enemy turn



                        
                        if (repeat != true)
                        {
                            bshield = bstartshield;

                            if (monster == "Scorched Soul" && baffected[0] == 0)
                            {
                                baffected[0] = 1;
                            }
                            //Status Effects

                            //Burning
                            if (bimmunities[0] == 0 && baffected[0] > 0)
                            {
                                Console.WriteLine(monster +"is burning!!\n"+monster+" take " + baffected[0] + "points of damage");
                                bhealth -= baffected[0];
                                baffected[0]--;

                            }
                            //Poisoned
                            if (bimmunities[1] == 0 && baffected[1] > 0)
                            {
                                Console.WriteLine(monster +"is poisoned!!\n"+monster+" take " + baffected[1] + "points of damage");
                                bhealth -= baffected[1];
                                baffected[1]--;
                            }
                            //Bleeding
                            if (bimmunities[2] == 0 && baffected[2] > 0)
                            {
                                Console.WriteLine(monster +"is bleeding!!\n"+monster+" take " + baffected[2] + "points of damage");
                                bhealth -= baffected[2];
                                baffected[2]--;
                            }
                            Console.WriteLine("\n" + monster + " turn\n");
                            Console.WriteLine("The " + monster + " " + baction);
                            switch (baction)
                            {
                                case "attack":

                                    for (int i = batknum; i > 0; i--)
                                    {
                                        int critroll = rng.Next(1, 101);
                                        if (critroll <= bcritchance)
                                        {
                                            crit = 2;
                                            Console.WriteLine("Crit!!");
                                        }
                                        else crit = 1;
                                        Console.WriteLine(monster + " deal " + (bdam * crit) + " points of damage");
                                        if (bloc >= bdam * crit)
                                        {
                                            Console.WriteLine("The attack was blocked completely!!");
                                        }
                                        else
                                        {
                                            health -= (bdam * crit - bloc);
                                        }
                                        bloc -= (bdam * crit);
                                        crit = 1;
                                        for(int j = 0; j < 3; j++)
                                        {
                                            if (bstatchance[j] >= rng.Next(1,101))
                                            {
                                                affected[j]++;
                                            }
                                        }
                                        
                                    }
                                    

                                    break;
                                case "block":
                                    bshield += bbloc;
                                    Console.WriteLine(monster + " shielded for " + bbloc);
                                    break;
                                case "heal":
                                    bhealth += bheal;
                                    Console.WriteLine(monster + " heal " + bheal + " points of damage");
                                    if (bhealth > maxbhealth)
                                    {
                                        bhealth = maxbhealth;
                                    }
                                    break;
                                default:
                                    Console.WriteLine("Error in enemy actions");
                                    break;

                            }
                        }

                        //Discard to Deck

                        if (discardsize == decksize)
                        {
                            for (int i = 0; i < decksize; i++)
                            {
                                deck[i] = discard[i];
                            }
                            discardsize = 0;
                        }

                        //Win/Lose

                        if (health <= 0)
                        {
                            Console.WriteLine("\nGame Over\n \nContinue?\n1: Yes\n2: No");
                            string retry = Console.ReadLine();
                            if (retry == "2")
                            {
                                playing = false;
                            }
                            else if (retry != "1")
                            {
                                Console.WriteLine("Restarting Anyway");
                            }
                        }
                        else if (bhealth <= 0)
                        {
                            Console.WriteLine("\nYou Win!!!");
                            money += price;
                        }
                        if (items[2] > 0)
                        {
                            critchance += items[2];
                        }
                    }
                    

                    //Reshuffle for the beggining of the next combat

                    //Pass deck to discard pile
                    for(int i = 0; i < deck.Length; i++)
                    {
                        if(deck[i] != "null")
                        {
                            discard[discardsize] = deck[i];
                            discardsize++;
                        }
                    }
                    //Pass hand to discard pile
                    for(int i = 0; i < hand.Length; i++)
                    {
                        discard[discardsize] = hand[i];
                        discardsize++;
                    }
                    //Fill the discard pile with null
                    for(int i = discardsize; i < deck.Length; i++)
                    {
                        discard[i] = "null";
                    }
                    //Pass discard pile to deck
                    for (int i = 0; i < deck.Length; i++)
                    {
                        deck[i] = discard[i];
                    }
                    Console.WriteLine("\nPress anything to continue");
                    Console.ReadLine();
                    Console.Clear();

                    //Elven Wooden Shield
                    if (items[0] > 0)
                    {
                        startshield = 1* items[0];
                    }
                    //Small Refilling Potion
                    if (items[1] > 0)
                    {
                        health += 2*items[1];
                        
                        if (health > maxhealth)
                        {
                            health = maxhealth;
                        }
                        
                    }
                    //Evolutionary Cell
                    if (items[2] > 0)
                    {
                        critchance = 10;
                        //Every turn the player gains 1% * nº of EC of critchance for the combat
                    }
                    //Dragon's Heart
                    if (items[3] > 0)
                    {
                        immunities[0] = 1;
                    }
                    //Snake Eyes
                    if (items[4] > 0)
                    {
                        immunities[1] = 1;
                    }
                    //SOwOrd
                    if (items[5] > 0)
                    {
                        startatkbonus = 1 * items[5];
                    }
                    //Titan Milk
                    if (items[6] > 0)
                    {
                        immunities[2] = 1;
                    }
                }



                //Bossfight (Room 10)

                for (int room = 10; room <= 10; room++)
                {

                    Console.WriteLine("Room " + room);
                    Console.WriteLine("Money: " + money);

                    
                    string[] discard = new string[deck.Length];
                    string[] hand = new string[5];
                    string[] monsters = { "Bone Golem", "Nature Beast" , "Dragon", "Goblin King" };
                    string[] bactionlist = { "attack", "block", "heal" };
                    Random rng = new Random();
                    int bhealth = 1, bdam = 0, bbloc = 0, bshield = 0, card, maxbhealth, batknum = 0, bstartshield = 0, bheal = 0, price = 0,bcritchance = 0, crit=1, discardsize = 0, decksize =0 ;
                    int[] bimmunities = { 0, 0, 0 }, bstatchance = { 0, 0, 0 }, baffected = { 0, 0, 0 }, affected = { 0, 0, 0 ,0,0,0};

                    //Counting deck size

                    for (int i = 0; i < deck.Length; i++)
                    {
                        if (deck[i] != "null")
                        {
                            decksize++;
                        }
                    }

                    //Enemy Selection

                    string monster = monsters[rng.Next(monsters.Length)];
                    switch (monster)
                    {
                        case "Bone Golem":
                            bhealth = 25;
                            bdam = 6;
                            batknum = 1;
                            bbloc = 3;
                            bstartshield = 2;
                            bheal = 3;
                            bcritchance = 15;
                            price = rng.Next(24, 32);
                            bimmunities[1] = 1;
                            bimmunities[2] = 1;
                            break;
                        case "Nature Beast":
                            bhealth = 22;
                            bdam = 4;
                            batknum = 2;
                            bbloc = 4;
                            bstartshield = 0;
                            bheal = 7;
                            bcritchance = 10;
                            price = rng.Next(26, 37);
                            bimmunities[1] = 1;
                            break;
                        case "Dragon":
                            bhealth = 30;
                            bdam = 8;
                            batknum = 1;
                            bbloc = 6;
                            bstartshield = 0;
                            bheal = 2;
                            bcritchance = 10;
                            price = rng.Next(28, 41);
                            bimmunities[0] = 1;
                            bstatchance[0] = 70;
                            break;
                        case "Goblin King":
                            bhealth = 23;
                            bdam = 4;
                            batknum = 1;
                            bbloc = 3;
                            bstartshield = 0;
                            bheal = 4;
                            bcritchance = 5;
                            price = rng.Next(20, 27);
                            bstatchance[1] = 20;
                            break;
                        
                        default:
                            Console.WriteLine("Error selecting monster");
                            break;
                    }

                    maxbhealth = bhealth;
                    bshield = bstartshield;

                    // Draw 5 cards from deck

                    for (int i = 0; i < hand.Length; i++)
                    {
                        card = rng.Next(decksize);

                        if (deck[card] != "null")
                        {
                            hand[i] = deck[card];
                            deck[card] = "null";
                        }
                        else
                        {
                            i--;
                        }
                    }


                    //Fight

                    while (health > 0 && bhealth > 0)
                    {
                        atkbonus = startatkbonus;
                        bloc = startshield;
                        //Status Effects

                        //Burning
                        if (immunities[0] == 0 && affected[0] > 0)
                        {
                            Console.WriteLine("You are burning!!\nYou take " + affected[0] + " points of damage");
                            health -= affected[0];
                            affected[0]--;

                        }
                        //Poisoned
                        if (immunities[1] == 0 && affected[1] > 0)
                        {
                            Console.WriteLine("You are poisoned!!\nYou take " + affected[1] + " points of damage");
                            health -= affected[1];
                            affected[1]--;
                        }
                        //Bleeding
                        if (immunities[2] == 0 && affected[2] > 0)
                        {
                            Console.WriteLine("You are bleeding!!\nYou take " + affected[2] + " points of damage");
                            health -= affected[2];
                            affected[2]--;
                        }
                        //Regen
                        if (affected[3] > 0)
                        {
                            Console.WriteLine("Your wounds are closing!!\nYou heal " + affected[3] + " points of damage");
                            health += affected[3];
                            affected[3]--;
                        }
                        //Strength
                        if (affected[4] > 0)
                        {
                            Console.WriteLine("Your muscles become stronger!!\nYour attacks dealed this round " + affected[4] + " extra points of damage");
                            atkbonus += affected[4];
                            affected[4]--;
                        }
                        //Resistance
                        if (affected[5] > 0)
                        {
                            Console.WriteLine("Your skin becomes thicker!!\nYour shield is " + affected[5] + " extra points of damage higher this round");
                            bloc += affected[5];
                            affected[5]--;
                        }


                        string baction = bactionlist[rng.Next(3)];
                        Console.WriteLine("\nYou:\nHp: " + health + "\nShield: " + bloc + "\n \n" + monster + "\nHp: " + bhealth + "\nShield: " + bshield + "\n" + monster + " is going to " + baction);
                        Console.WriteLine("\nYour hand is 1:" + hand[0] + " 2:" + hand[1] + " 3:" + hand[2] + " 4:" + hand[3] + " 5:" + hand[4]);
                        Console.WriteLine("\nYour turn\nPlease select a card to play");
                        string action = Console.ReadLine();
                        bool repeat = false;
                        Console.WriteLine();
                        //Player turn

                        switch (action)
                        {
                            case "1":
                            case "2":
                            case "3":
                            case "4":
                            case "5":
                                string play = hand[int.Parse(action) - 1];
                                switch (play)
                                {
                                    case "Heal":
                                        health += 5;
                                        Console.WriteLine("You heal " + 5 + " points of damage");
                                        if (health > maxhealth)
                                        {
                                            health = maxhealth;
                                        }
                                        break;
                                    case "Slash":

                                        int atk = 5, critroll = rng.Next(1, 101);
                                        if (critroll <= critchance)
                                        {
                                            crit = 2;
                                            Console.WriteLine("Crit!!");
                                        }
                                        else crit = 1;
                                        Console.WriteLine("You deal " + ((atk + atkbonus) * crit) + " points of damage");
                                        if (bshield >= (atk + atkbonus) * crit)
                                        {
                                            Console.WriteLine(monster + " blocked the attack completely!!");
                                        }
                                        else
                                        {
                                            bhealth -= ((atk + atkbonus) * crit - bshield);
                                        }
                                        bshield -= ((atk + atkbonus) * crit);
                                        crit = 1;
                                        break;
                                    case "Cross Cut":

                                        for (int i = 1; i <= 2; i++)
                                        {
                                            int atk3 = 4, critroll3 = rng.Next(1, 101);
                                            if (critroll3 <= critchance)
                                            {
                                                crit = 2;
                                                Console.WriteLine("Crit!!");
                                            }
                                            else crit = 1;
                                            Console.WriteLine("You deal " + ((atk3 + atkbonus) * crit) + " points of damage");
                                            if (bshield >= (atk3 + atkbonus) * crit)
                                            {
                                                Console.WriteLine(monster + " blocked the attack completely!!");
                                            }
                                            else
                                            {
                                                bhealth -= ((atk3 + atkbonus) * crit - bshield);
                                            }
                                            bshield -= ((atk3 + atkbonus) * crit);
                                            crit = 1;
                                        }
                                        break;
                                    case "Block":
                                        bloc += 4;
                                        Console.WriteLine("You shielded for " + bloc);
                                        break;
                                    case "Smash":

                                        int atk2 = 11, critroll2 = rng.Next(1, 101);
                                        if (critroll2 <= critchance)
                                        {
                                            crit = 2;
                                            Console.WriteLine("Crit!!");
                                        }
                                        else crit = 1;
                                        Console.WriteLine("You deal " + ((atk2 + atkbonus) * crit) + " points of damage");
                                        if (bshield >= (atk2 + atkbonus) * crit)
                                        {
                                            Console.WriteLine(monster + " blocked the attack completely!!");
                                        }
                                        else
                                        {
                                            bhealth -= ((atk2 + atkbonus) * crit - bshield);
                                        }
                                        bshield -= ((atk2 + atkbonus) * crit);
                                        crit = 1;
                                        break;
                                    case "Deep Wounds":

                                        int atk4 = 8, critroll4 = rng.Next(1, 101);
                                        if (critroll4 <= critchance)
                                        {
                                            crit = 2;
                                            Console.WriteLine("Crit!!");
                                        }
                                        else crit = 1;
                                        Console.WriteLine("You deal " + ((atk4 + atkbonus) * crit) + " points of damage");
                                        if (bshield >= (atk4 + atkbonus) * crit)
                                        {
                                            Console.WriteLine(monster + " blocked the attack completely!!");
                                        }
                                        else
                                        {
                                            bhealth -= ((atk4 + atkbonus) * crit - bshield);
                                            if (30 >= rng.Next(1, 101))
                                            {
                                                baffected[2] = 1;
                                                Console.WriteLine(monster + " is bleeding");
                                            }
                                        }
                                        bshield -= ((atk4 + atkbonus) * crit);
                                        crit = 1;
                                        break;
                                    case "Fire Ball":

                                        int atk5 = 10, critroll5 = rng.Next(1, 101);
                                        if (critroll5 <= critchance)
                                        {
                                            crit = 2;
                                            Console.WriteLine("Crit!!");
                                        }
                                        else crit = 1;
                                        Console.WriteLine("You deal " + ((atk5 + atkbonus) * crit) + " points of damage");
                                        if (bshield >= (atk5 + atkbonus) * crit)
                                        {
                                            Console.WriteLine(monster + " blocked the attack completely!!");
                                        }
                                        else
                                        {
                                            bhealth -= ((atk5 + atkbonus) * crit - bshield);
                                            if (20 >= rng.Next(1, 101))
                                            {
                                                baffected[0] = 1;
                                                Console.WriteLine(monster + " is burning");
                                            }
                                        }
                                        bshield -= ((atk5 + atkbonus) * crit);
                                        crit = 1;
                                        break;
                                    case "Poison Dart":

                                        int atk6 = 1, critroll6 = rng.Next(1, 101);
                                        if (critroll6 <= critchance)
                                        {
                                            crit = 2;
                                            Console.WriteLine("Crit!!");
                                        }
                                        else crit = 1;
                                        Console.WriteLine("You deal " + ((atk6 + atkbonus) * crit) + " points of damage");
                                        if (bshield >= (atk6 + atkbonus) * crit)
                                        {
                                            Console.WriteLine(monster + " blocked the attack completely!!");
                                        }
                                        else
                                        {
                                            bhealth -= ((atk6 + atkbonus) * crit - bshield);
                                            if (100 >= rng.Next(1, 101))
                                            {
                                                baffected[1] = 7;
                                                Console.WriteLine(monster + " is poisoned");
                                            }
                                        }
                                        bshield -= ((atk6 + atkbonus) * crit);
                                        crit = 1;
                                        break;
                                    case "Iron Skin":
                                        affected[5] = 3;
                                        bloc += 4;
                                        Console.WriteLine("You have resistance");
                                        Console.WriteLine("You shielded for " + bloc);
                                        break;
                                    case "Natural Healing":
                                        affected[3] = 5;
                                        Console.WriteLine("You have regeneration");
                                        break;
                                    case "Wrath":
                                        for (int i = 1; i <= 4; i++)
                                        {
                                            atkbonus++;
                                            int atk7 = 0, critroll7 = rng.Next(1, 101);
                                            if (critroll7 <= critchance)
                                            {
                                                crit = 2;
                                                Console.WriteLine("Crit!!");
                                            }
                                            else crit = 1;
                                            Console.WriteLine("You deal " + ((atk7 + atkbonus) * crit) + " points of damage");
                                            if (bshield >= (atk7 + atkbonus) * crit)
                                            {
                                                Console.WriteLine(monster + " blocked the attack completely!!");
                                            }
                                            else
                                            {
                                                bhealth -= ((atk7 + atkbonus) * crit - bshield);
                                                affected[4]++;
                                            }
                                            bshield -= ((atk7 + atkbonus) * crit);
                                            crit = 1;
                                        }
                                        break;
                                    case "Vampiric Touch":
                                        int atk8 = 11, critroll8 = rng.Next(1, 101);
                                        if (critroll8 <= critchance)
                                        {
                                            crit = 2;
                                            Console.WriteLine("Crit!!");
                                        }
                                        else crit = 1;
                                        Console.WriteLine("You deal " + ((atk8 + atkbonus) * crit) + " points of damage");
                                        if (bshield >= (atk8 + atkbonus) * crit)
                                        {
                                            Console.WriteLine(monster + " blocked the attack completely!!");
                                        }
                                        else
                                        {
                                            bhealth -= ((atk8 + atkbonus) * crit - bshield);
                                            health += ((atk8 + atkbonus) * crit - bshield);
                                        }
                                        bshield -= ((atk8 + atkbonus) * crit);
                                        crit = 1;
                                        if (health > maxhealth)
                                        {
                                            health = maxhealth;
                                        }
                                        break;

                                    default:
                                        Console.WriteLine("Error in player cards");
                                        break;

                                }

                                card = rng.Next(decksize);
                                while (deck[card] == "null")
                                {
                                    card = rng.Next(decksize);
                                }

                                hand[int.Parse(action) - 1] = deck[card];
                                deck[card] = "null";
                                discard[discardsize] = play;
                                discardsize++;

                                break;

                            default:
                                Console.WriteLine("Please select a valid action");
                                repeat = true;
                                break;


                        }

                        //Enemy turn



                        
                        if (repeat != true)
                        {
                            bshield = bstartshield;
                            //Burning
                            if (bimmunities[0] == 0 && baffected[0] > 0)
                            {
                                Console.WriteLine(monster + "is burning!!\n" + monster + " take " + baffected[0] + "points of damage");
                                bhealth -= baffected[0];
                                baffected[0]--;

                            }
                            //Poisoned
                            if (bimmunities[1] == 0 && baffected[1] > 0)
                            {
                                Console.WriteLine(monster + "is poisoned!!\n" + monster + " take " + baffected[1] + "points of damage");
                                bhealth -= baffected[1];
                                baffected[1]--;
                            }
                            //Bleeding
                            if (bimmunities[2] == 0 && baffected[2] > 0)
                            {
                                Console.WriteLine(monster + "is bleeding!!\n" + monster + " take " + baffected[2] + "points of damage");
                                bhealth -= baffected[2];
                                baffected[2]--;
                            }
                            Console.WriteLine("\n" + monster + " turn");
                            Console.WriteLine("The " + monster + " " + baction);
                            switch (baction)
                            {
                                case "attack":

                                    for (int i = batknum; i > 0; i--)
                                    {
                                        int critroll = rng.Next(1, 101);
                                        if (critroll <= bcritchance)
                                        {
                                            crit = 2;
                                            Console.WriteLine("Crit!!");
                                        }
                                        else crit = 1;
                                        Console.WriteLine(monster + " deal " +( bdam*crit) + " points of damage");
                                        if (bloc >= bdam*crit)
                                        {
                                            Console.WriteLine("The attack was blocked completely!!");
                                        }
                                        else
                                        {
                                            health -= (bdam*crit - bloc);
                                        }
                                        bloc -= bdam*crit;
                                        crit = 1;
                                        for (int j = 0; j < 3; j++)
                                        {
                                            if (bstatchance[j] >= rng.Next(1, 101))
                                            {
                                                affected[j]++;
                                            }
                                        }
                                    }


                                    break;
                                case "block":
                                    bshield += bbloc;
                                    Console.WriteLine(monster + " shielded for " + bbloc);
                                    break;
                                case "heal":
                                    bhealth += bheal;
                                    Console.WriteLine(monster + " heal " + bheal + " points of damage");
                                    if (bhealth > maxbhealth)
                                    {
                                        bhealth = maxbhealth;
                                    }
                                    break;
                                default:
                                    Console.WriteLine("Error in enemy actions");
                                    break;

                            }
                        }

                        //Discard pile to Deck

                        if (discardsize == decksize)
                        {
                            for (int i = 0; i < decksize; i++)
                            {
                                deck[i] = discard[i];
                            }
                            discardsize = 0;
                        }

                        //Win/Lose

                        if (health <= 0)
                        {
                            Console.WriteLine("\nGame Over\n \nContinue?\n1: Yes\n2: No");
                            string retry = Console.ReadLine();
                            if (retry == "2")
                            {
                                playing = false;
                            }
                            else if (retry != "1")
                            {
                                Console.WriteLine("Restarting Anyway");
                            }
                        }
                        else if (bhealth <= 0)
                        {
                            Console.WriteLine("\nYou Win!!!");
                        }

                    }
                    money += price;

                    //Reshuffle for the beggining of the next combat

                    //Pass deck to discard pile
                    for (int i = 0; i < deck.Length; i++)
                    {
                        if (deck[i] != "null")
                        {
                            discard[discardsize] = deck[i];
                            discardsize++;
                        }
                    }
                    //Pass hand to discard pile
                    for (int i = 0; i < hand.Length; i++)
                    {
                        discard[discardsize] = hand[i];
                        discardsize++;
                    }
                    //Fill the discard pile with null
                    for (int i = discardsize; i < deck.Length; i++)
                    {
                        discard[i] = "null";
                    }
                    //Pass discard pile to deck
                    for (int i = 0; i < deck.Length; i++)
                    {
                        deck[i] = discard[i];
                    }
                    Console.WriteLine("\nPress anything to continue");
                    Console.ReadLine();
                    Console.Clear();
                }

                Console.WriteLine("Congratuations!!!\nYou cleared the dungeon with " + money + " money!!!");

                //Restart

                Console.WriteLine("\nPlay Again?\n1: Yes\n2: No");
                string restart = Console.ReadLine();
                if (restart == "2")
                {
                    playing = false;
                }
                else if (restart != "1")
                {
                    Console.WriteLine("Restarting Anyway");
                }
            }
            
        }
    }
}
