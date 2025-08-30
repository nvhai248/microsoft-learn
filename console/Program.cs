// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

OperatingSystem thisOS = Environment.OSVersion;
Console.WriteLine(thisOS.Platform);
Console.WriteLine(thisOS.VersionString);

// Some of data type
int a = 1;
char c = 'A';
float f = 1.02f;
decimal d = 400.85m;
int b = default;
bool tf = default;

Console.WriteLine($"{a}, {c}, {f}, {d}, {b}, {tf}");
Console.WriteLine($"{a + c}");
Console.WriteLine($"{(char)(a + c)}");
Console.WriteLine($"{a + f}");
Console.WriteLine($"{c + f}");

void PrintStruct(MyStruct s)
{
    Console.WriteLine($"{s.a}, {s.b}");
}

MyStruct s1;
s1.a = 2;
s1.b = false;
PrintStruct(s1);

MyClass c1 = new MyClass();
c1.a = 3;
c1.b = false;
c1.print();

Console.WriteLine($"a + c = {a + c}");
Console.WriteLine($"{0,-15} {1,10}", "Float Val", "Int Val");
Console.WriteLine($"{0,-15} {1,10}", f, a);

// -----------------------------
// GarBage Collection
// -----------------------------

void DoSomeBigOperation()
{
    byte[] myArray = new byte[1000000];
    Console.WriteLine($"Allocated memory is: {GC.GetTotalMemory(false)}");
    Console.ReadLine();
}

// Retrieve and print the total memory allocated
Console.WriteLine($"Allocated memory is: {GC.GetTotalMemory(false)}");
Console.ReadLine();

DoSomeBigOperation();
GC.Collect();

// Retrieve and print the total memory allocated
Console.WriteLine($"Allocated memory is: {GC.GetTotalMemory(false)}");
Console.ReadLine();

string uname;
Console.WriteLine("What your name?");
uname = Console.ReadLine();
Console.WriteLine($"Your name is: {uname}");

// -----------------------------
// TYPE DECLARATIONS AT THE END
// -----------------------------
class MyClass
{
    public int a;
    public bool b;

    public void print()
    {
        // This is string interpolation
        Console.WriteLine($"{this.a}, {this.b}");
    }
}

struct MyStruct
{
    public int a;
    public bool b;
}