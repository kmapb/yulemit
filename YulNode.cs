using System.Collections.Generic;

abstract class Node {
    abstract public IEnumerable<string> emit();
}

abstract class Statement : Node {
}

class Block : Statement {
    protected List<Statement> statements_;
    public Block(List<Statement> statements) {
        statements_ = statements;
    }
    override public IEnumerable<string> emit() {
        yield return "{";
        foreach (var stmt in statements_) {
            foreach (var s in stmt.emit()) {
                yield return s;
            }
        }
        yield return "}";
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

    public override IEnumerable<string> emit() {
        yield return name_;
        yield return "(";
        for (int i = 0; i < params_.Count; i++)
        {
            var p = params_[i];
            foreach (var s in p.emit()) yield return s;
            if (i < params_.Count - 1) yield return ", ";
        }
        yield return ")";
    }
}

class BuiltinCallStatement : Statement {
    private BuiltinCallExpr expr_;
    public BuiltinCallStatement(string name, List<Expression> pa){
        expr_ = new BuiltinCallExpr(name, pa);
    }

    public override IEnumerable<string> emit() {
        return expr_.emit();
    }
}

enum Type {

}

/*
 * TypedParam is not a general node; it only appears in function
 * definitions.
 */
class TypedParam {
    public Type? type_;
    public string name_;

    public TypedParam(Type? type, string name) {
        type_ = type;
        name_ = name;
    }
    public static string emitType(Type type){
        return ""; // TODO: there are like multiple types buddies
    }
}

class FunctionDefinitionStatement : Statement {
    protected string name_;
    protected List<TypedParam> params_;
    protected List<TypedParam> returns_;
    protected Block body_;
    public FunctionDefinitionStatement(string name, List<TypedParam> ps, List<TypedParam> returns, Block body){
        name_ = name;
        params_ = ps;
        returns_ = returns;
        body_ = body;
    }

    private static  IEnumerable<string> emitTypedParamList(List<TypedParam> tps) {
        for (var i = 0; i < tps.Count; i++){
            var tp = tps[i];
            yield return tp.name_;
            var typeVal = tp.type_;
            if (typeVal is not null){
                yield return ":";
                yield return TypedParam.emitType((Type)typeVal);
            }
            if (i < tps.Count - 1) yield return ", ";
        }
    }
    override public IEnumerable<string> emit() {
        yield return "function " ;
        yield return name_;
        yield return "(";
        foreach (var s in emitTypedParamList(params_)) yield return s;
        yield return ")";
        if (returns_.Count > 0) {
            yield return " -> ";
            foreach (var s in emitTypedParamList(returns_)) yield return s;
        }
        foreach (var s in body_.emit()) yield return s;
    }
}
class VarExpr : Expression {
    private string name_;
    public VarExpr(string name){
        name_ = name;
    }
    override public IEnumerable<string> emit() {
        yield return name_;
    }
}

class IfStatement : Statement {
    private Expression cond_;
    private Statement body_;
    public IfStatement(Expression cond, Statement body){
        cond_ = cond;
        body_ = body;
    }

    override public IEnumerable<string> emit() {
        yield return "if (";
        foreach (var s in cond_.emit()) yield return s;
        yield return ") ";
        foreach (var s in body_.emit()) yield return s;
    }
}