public class StepByStepSolutionToHtml
{
   // фабрика, конвертирует xml строку с комментариями подробного 
   // решения в xml строку, пригодную для класса отрисовки:
   SBSXmlCmntsToXmlStringRuleFactory _factory = new SBSXmlCmntsToXmlStringRuleFactory();

   // экземпляр объекта для отрисовки строки специального xml формата 
   // в svg:
   IPrint _print = new PrintVector();

   string SingleStepToHtml(string cmnts, string expr)
   {
       // преобразуем комментарии из xml строки в массив 
       // xml строк специального формата, природных для 
       // класса отрисовки:
       string[] cmnts = _factory.CreateInstance(sbs.Cmnts);

       // преобразуем комментарии в строку:
       res += "<div class='cmnts'>";

       // преобразуем каждый элемент массива:
       foreach(string line in cmnts)
       {
           res += "<div class='cmnts-line'>";
           res += _print.Print(line);
           res += "</div>";
       }

       res += "</div>";

       // теперь математическое выражение:
       res += "<div class='expr'>";
       res += _print.Print(sbs.Expr);
       res += "</div>";

       // возвращаем результат:
       return res;
   }

   ///<summary>Преобразует объект подробного решения в
   ///html строку</summary>
   public string ToHtml(StepByStepSolution sbs)
   {
       // у нас корень дерева и его child Nodes
       // на самом деле находятся на одном уровне =>
       // преобразуем корень отдельно:
       string res = SingleStepToHtml(sbs.Cmnts, sbs.Expr);

       // теперь проходим по всем потомкам данного 
       // узла и преобразуем их:
       for(int i = 0; i < sbs.ChildNodes.Count; i++)
       {
           res += ToHtmlInner(sbs.ChildNodes[i]);
       }

       // возвращаем результат:
       return res;
   }

   string ToHtmlInner(StepByStepSolution sbs)
   {
       // результат:
       string res = "";
       // будем обходить дерево снизу =>
       // "забуряемся" вниз дерева:
       for(int i=0; i < sbs.ChildNodes.Count; i++)
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
