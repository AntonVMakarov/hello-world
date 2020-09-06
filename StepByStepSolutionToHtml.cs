using CASVer3;
using CASVer3.Render;

public class StepByStepSolutionToHtml
{
    // фабрика, конвертирует xml строку с комментариями подробного 
    // решения в xml строку, пригодную для класса отрисовки:
    SBSXmlCmntsToXmlStringRuleFactory _factory = new SBSXmlCmntsToXmlStringRuleFactory();

    /// <summary>экземпляр объекта для отрисовки математических выражений в svg</summary>
    IPrint _printExpr;

    /// <summary>экземпляр объекта для отрисовки комментариев в svg</summary>
    IPrint _printCmnts;

    /// <summary>открытый конструктор</summary>
    public StepByStepSolutionToHtml()
    {
        // создаем экземпляр объекта для отрисовки математических выражений:
        _printExpr = new PrintVector(new MathRenderDefaultSchemeVector());

        // создаем экземпляр объекта для отрисовки комментариев:
        _printCmnts = new PrintVector(new MathRenderStandartCommentsSchemeVector());
    }

    /// <summary>преобразует строки в html</summary>
    /// <param name="entity">входная строка</param>
    /// <param name="parentDivClass">имя css класса родительского div элемента</param>
    /// <param name="lineDivClass">имя css класс div элемента, соответствующего каждой строке</param>
    /// <param name="coverXmlTagName">имя xml тега в который мы оборачиваем входную строку для корректного дальнейшего парсинга</param>
    /// <param name="print">экземпляр объекта для отрисовки строки специального xml формата в svg</param>
    string EntityToHtml(string entity, string parentDivClass, string lineDivClass, string coverXmlTagName, IPrint print)
    {
        // преобразуем комментарии из xml строки в массив 
        // xml строк специального формата, природных для 
        // класса отрисовки:
        string[] cmntsArray = _factory.CreateInstance("<" + coverXmlTagName + ">" + entity + "</" + coverXmlTagName + ">");

        // преобразуем комментарии в строку:
        string res = "<div class='" + parentDivClass + "'>";

        // преобразуем каждый элемент массива:
        foreach (string line in cmntsArray)
        {
            res += "<div class='" + lineDivClass + "'>";
            res += print.Print(line);
            res += "</div>";
        }

        // закрываем тег div:
        res += "</div>";

        // возвращем результат:
        return res;
    }

    //<summary>Преобразует шаг комментариев в html формат</summary>
    string SingleStepToHtml(string cmnts, string expr)
    {
        // преобразуем комментарии из xml строки в html:
        string res = EntityToHtml(cmnts, "cmnts", "cmnts-line", "cmnts", _printCmnts);

        // преобразуем математические выражения в html:
        res += EntityToHtml(expr, "expr", "expr-line", "math", _printExpr);

        // возвращаем результат:
        return res;
    }

    ///<summary>Преобразует объект подробного решения в
    ///html строку</summary>
    public string ToHtml(StepByStepNTreeNode sbs)
    {
        // у нас корень дерева и его child Nodes
        // на самом деле находятся на одном уровне =>
        // преобразуем корень отдельно:
        string res = SingleStepToHtml(sbs.Cmnts, sbs.Expr);

        // теперь проходим по всем потомкам данного 
        // узла и преобразуем их:
        for (int i = 0; i < sbs.ChildNodes.Count; i++)
        {
            res += ToHtmlInner(sbs.ChildNodes[i]);
        }

        // возвращаем результат:
        return res;
    }

    ///<summary>Рекурсивная функция для преобразования всех внутренних 
    ///шагов в html формат</summary>
    string ToHtmlInner(StepByStepNTreeNode sbs)
    {
        // результат:
        string res = "";
        // будем обходить дерево снизу =>
        // "забуряемся" вниз дерева:
        for (int i = 0; i < sbs.ChildNodes.Count; i++)
        {
            // получается childNodes - это вложенные решения:
            res += "<div class='inner-step'>";
            res += ToHtmlInner(sbs.ChildNodes[i]);
            res += "</div>";
        }

        // тут, получается мы уже в самом низу дерева
        // больше нет никаких потомков => начинаем обработку 
        // текущего узла:
        res += SingleStepToHtml(sbs.Cmnts, sbs.Expr);

        // возвращаем результат:
        return res;
    }
}
