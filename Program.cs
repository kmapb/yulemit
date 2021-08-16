
var b = new Block(new List<Statement>());
b.addStatement(new IfStatement(new VarExpr("b"),
    new BuiltinCallStatement("foo", new List<Expression>())));

Console.WriteLine("yulEmit v -0.5 + i");
foreach (var s in  b.emit()) {
    Console.Write(s);
}
Console.WriteLine("/* fin */");