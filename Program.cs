// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
var b = new Block(new List<Statement>());
Console.WriteLine("Statement:" + b.emit());