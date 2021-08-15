abstract class Node {
    abstract public string emit();
}

abstract class Statement : Node {
}

class Block : Statement {
    protected List<Statement> statements_;
    public Block(List<Statement> statements) {
        statements_ = statements;
    }
    override public string emit() {
        var s = "";
        foreach (var stmt in statements_) {
            s += stmt.emit();
        }
        return s;
    }
    public void addStatement(Statement s){
        statements_.Append(s);
    }
}