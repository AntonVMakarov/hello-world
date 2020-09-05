public class StepByStepSolutionToHtml
{
   public string ToHtml(StepByStepSolution sbs)
   {
       // результат:
       string res = "";
       // будем обходить дерево снизу =>
       // "забуряемся" вниз дерева:
       for(int i=0; i < sbs.ChildNodes.Count; i++)
       {
           // получается childNodes - это вложенные решения:
           res += "<div class='inner-step'>";
           res += ToHtml(sbs.ChildNodes[i]);
           res += "</div>";
       }

       // тут, получается мы уже в самом низу дерева
       // больше нет никаких потомков => начинаем обработку 
       // текущего узла:
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
}
