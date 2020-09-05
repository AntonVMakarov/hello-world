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
           res += ToHtml(sbs.ChildNodes[i]);
       }

       // тут, получается мы уже в самом низу дерева
       // больше нет никаких потомков => начинаем обработку 
       // текущего узла:
   }
}
