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
        var s = "{";
        foreach (var stmt in statements_) {
            s += stmt.emit();
        }
        return s + "}";
    }
    public void addStatement(Statement s){
        statements_.Add(s);
    }
}

abstract class Expression : Node {

}

class BuiltinCallExpr : Expression {
    string name_;
    List<Expression> params_;

    public BuiltinCallExpr(string name, List<Expression> p){
        name_ = name;
        params_ = p;
    }

    public override string emit() {
        var s = name_ + "(";
        for (int i = 0; i < params_.Count; i++)
        {
            var p = params_[i];
            s += p.emit();
            if (i < params_.Count - 1) s += ", ";
        }
        return s + ")";
    }
}

class BuiltinCallStatement : Statement {
    private BuiltinCallExpr expr_;
    public BuiltinCallStatement(string name, List<Expression> pa){
        expr_ = new BuiltinCallExpr(name, pa);
    }

    public override string emit() {
        return expr_.emit();
    }
}
class VarExpr : Expression {
    private string name_;
    public VarExpr(string name){
        name_ = name;
    }
    override public string emit() {
        return name_;
    }
}

class IfStatement : Statement {
    private Expression cond_;
    private Statement body_;
    public IfStatement(Expression cond, Statement body){
        cond_ = cond;
        body_ = body;
    }

    override public string emit() {
        return "if (" + cond_.emit() + ") " + body_.emit();
    }
}