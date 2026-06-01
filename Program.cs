using System.Threading.Tasks;
Random rnd = new Random();

int level = 1;

int damage = 0;
int health = 0;
int defense = 0;
int dodge = 0;

int damageE = 0;
int healthE = 0;
int defenseE = 0;
int dodgeE = 0;

String type = "";
String typeE = "";

String input = "";

await start();

async Task start()
{
    damage = 0;
    health = 0;
    defense = 0;
    dodge = 0;
    level = 0;

    Console.WriteLine("Choose your gladiator by typing their name:");
    Console.WriteLine("jugger / dodger / spike");

    input = Console.ReadLine();

    if(input == "jugger")
    {
        damage = rnd.Next(40, 60);
        health = rnd.Next(60, 70);
        defense = rnd.Next(70, 90);
        dodge = rnd.Next(5, 12);

        type = "jugger";
    }
    else if(input == "dodger")
    {
        damage = rnd.Next(30, 50);
        health = rnd.Next(45, 60);
        defense = rnd.Next(25, 45);
        dodge = rnd.Next(50, 65);

        type = "dodger";
    }
    else if(input == "spike")
    {
        damage = rnd.Next(65, 80);
        health = rnd.Next(60, 80);
        defense = rnd.Next(50, 70);
        dodge = rnd.Next(10, 15);

        type = "spike";
    }
    else
    {
        Console.WriteLine("That is not a gladiator!");
        await start();
        return;
    }


    Console.WriteLine("Stats:");
    Console.Write("Damage:");
    Console.WriteLine(damage);
    Console.Write("Health:");
    Console.WriteLine(health);
    Console.Write("Defense");
    Console.WriteLine(defense);
    Console.Write("Dodge:");
    Console.WriteLine(dodge);
    
    Console.WriteLine("Enter arena? (y / n)");

    input = Console.ReadLine();
    if(input == "y"){await fight();}
    else{await start();}
}

void generateEnemy()
{
    int r = rnd.Next(1, 4);

    if(r == 1)
    {
        damageE = rnd.Next(40, 60);
        healthE = rnd.Next(60, 70);
        defenseE = rnd.Next(70, 90);
        dodgeE = rnd.Next(5, 12);

        typeE = "jugger";
    }
    else if(r == 2)
    {
        damageE = rnd.Next(30, 50);
        healthE = rnd.Next(45, 60);
        defenseE = rnd.Next(25, 45);
        dodgeE = rnd.Next(50, 65);

        typeE = "dodger";
    }
    else if(r == 3)
    {
        damageE = rnd.Next(65, 80);
        healthE = rnd.Next(60, 80);
        defenseE = rnd.Next(50, 70);
        dodgeE = rnd.Next(10, 15);

        typeE = "spike";
    }

    damageE += (int)(damageE * (level / 10.0));
    healthE += (int)(healthE * (level / 10.0));
    defenseE += (int)(defenseE * (level / 10.0));
}

async Task fight()
{
    generateEnemy();

    Console.WriteLine(type + " vs. " + typeE);
    Console.WriteLine("Level: " + level);
    await Task.Delay(2000);


    int h = health;
    int hE = healthE;
    int turn = 1;
    int d = 0;

    while(h > 0 && hE > 0)
    {
        if (turn % 2 == 0)
        {   
            Console.WriteLine(turn + ": Enemy turn: ");
            await Task.Delay(1000);
            int r = rnd.Next(1, 7);
            Console.WriteLine("Rolled: " + r);
            if(r < 6)
            {
                d = damageE + (damageE * r ) / 10;
            }
            else
            {
                Console.WriteLine("Critical hit!");
                d = damageE * 2;
            }

            await Task.Delay(1000);

            d = d / (defense / 10);

            r = rnd.Next(0, 100);
            if(r < dodge){Console.WriteLine("Player dogged!");}
            else
            {
                Console.WriteLine("Player recieved " + d + "damage!");
                h = h - d;
            }

            await Task.Delay(1000);

            Console.WriteLine("Player HP: " + h + " Enemy HP: " + hE);

            await Task.Delay(1000);

        }

        else
        {   
            Console.WriteLine(turn + ": Player turn: ");
            await Task.Delay(1000);
            int r = rnd.Next(1, 7);
            Console.WriteLine("Rolled: " + r);
            if(r < 6)
            {
                d = damage + (damage * r ) / 10;
            }
            else
            {
                Console.WriteLine("Critical hit!");
                d = damage * 2;
            }

            await Task.Delay(1000);

            d = d / (defenseE / 10);

            r = rnd.Next(0, 100);
            if(r < dodgeE){Console.WriteLine("Enemy dogged!");}
            else
            {
                Console.WriteLine("Enemy recieved " + d + "damage!");
                hE = hE - d;
            }

            await Task.Delay(1000);

            Console.WriteLine("Player HP: " + h + " Enemy HP: " + hE);

            await Task.Delay(1000);

        }
    

        turn ++;
    }

    if(h <= 0){await lost();}
    else if(hE <= 0){await won();}


}

async Task lost()
{
    Console.WriteLine("You lost. Try again? (y / n)");
    Console.WriteLine("Level: " + level);
    input = Console.ReadLine();
    if(input == "y"){await start();}
    else{System.Environment.Exit(1);}

}

async Task won()
{
    Console.WriteLine("You won. Continue? (y / n)");
    input = Console.ReadLine();
    if(input == "y"){await upgrade();}
    else{System.Environment.Exit(1);}
}

async Task upgrade()
{   
    level ++;
    Console.WriteLine("Upgrade your stats: damage / health / defense");
    Console.WriteLine("Current stats:");
    Console.Write("Damage:");
    Console.WriteLine(damage);
    Console.Write("Health:");
    Console.WriteLine(health);
    Console.Write("Defense");
    Console.WriteLine(defense);
    Console.Write("Dodge:");
    Console.WriteLine(dodge);

    input = Console.ReadLine();

    if(input == "damage"){damage += 10; await fight();}
    else if(input == "health"){health += 10; await fight();}
    else if(input == "defense"){defense += 10; await fight();}
    else{Console.WriteLine("That is not a stat!"); await upgrade(); return;}
}