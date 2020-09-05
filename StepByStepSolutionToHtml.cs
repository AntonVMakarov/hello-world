public class StepByStepSolutionToHtml
{
   string SingleStepToHtml(string cmnts, string expr)
   {
       // сначала преобразуем комментарии в строку:
       res += "<div class='cmnts'>";
       res += ConvertCmntsToHtml(sbs.Cmnts);
       res += "</div>";

       // теперь математическое выражение:
       res += "<div class='expr'>";
       res += ConvertExprToHtml(sbs.Expr);
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
       for(int i=0; i < sbs.ChildNodes.Count; i++)
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
