Console.WriteLine("Hello, World!");
var b = new Block(new List<Statement>());
b.addStatement(new IfStatement(new VarExpr("b"),
    new BuiltinCallStatement("foo", new List<Expression>())));
Console.WriteLine("Statement:" + b.emit());