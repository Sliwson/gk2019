using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;

namespace Polygons
{
    public class RelationCreator
    {
        private Label errorLabel;
        private (Edge, Edge) relatedEdges;
        private (TextBox, TextBox) informationTextboxes;

        public RelationCreator(Label errorLabel, (TextBox, TextBox) informationTextboxes)
        {
            this.errorLabel = errorLabel;
            this.informationTextboxes = informationTextboxes;

            relatedEdges = (null, null);
            errorLabel.Text = "";
        }

        public bool CanAddEdge(Edge edge)
        {
            if (relatedEdges.Item1 != null && relatedEdges.Item2 != null)
                return false;

            if (relatedEdges.Item1 == edge || relatedEdges.Item2 == edge)
                return false;

            if (edge.UnderlyingPolygon == null)
                return false;

            if (relatedEdges.Item1 != null)
            {
                if (edge == relatedEdges.Item1)
                    return false;
                else if (!edge.UnderlyingPolygon.IsEdgeInPolygon(relatedEdges.Item1))
                    return false;
            }
            else if (relatedEdges.Item2 != null)
            {
                if (edge == relatedEdges.Item2)
                    return false;
                else if (!edge.UnderlyingPolygon.IsEdgeInPolygon(relatedEdges.Item2))
                    return false;
            }

            return true;
        }

        public void AddEdge(Edge edge)
        {
            if (!CanAddEdge(edge))
            {
                errorLabel.Text = "You can't add this edge to relation creator";
                return;
            }

            if (relatedEdges.Item1 == null)
                relatedEdges.Item1 = edge;
            else if (relatedEdges.Item2 == null)
                relatedEdges.Item2 = edge;
        }
    }
}
