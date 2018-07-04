using System.Collections.Generic;

namespace Assets.HandShankAdapation.UIBounds
{
    public class UIContainer
    {

        private List<Edge> row;

        public List<Edge> Row
        {
            get
            {
                if (row == null) row = new List<Edge>();
                return row;
            }
        }

        public int Count
        {
            get
            {
               return Row.Count / 2;
            }
        }

       

        private List<Edge> column;

       public List<Edge> Column
       {
           get
           {
               if(column == null) column = new List<Edge>();
               return column;
           }
       }

        public void Sort()
        {
            Row.Sort();
            Column.Sort();
            for (int i = 0; i < Row.Count; i++)
            {
                Row[i].Index = i;
            }

            for (int j = 0; j < Column.Count; j++)
            {
                Column[j].Index = j;
            }
        }
        
      
       public void Insert(UIRect ui)
       {
           Row.Add(ui.LeftEdge);
           Row.Add(ui.RightEdge);
           Column.Add(ui.DownEdge);
           Column.Add(ui.UpEdge);
       }
   }
}
