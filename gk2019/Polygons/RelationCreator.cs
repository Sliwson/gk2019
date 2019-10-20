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

        public void InitEvents(Button cancelFirstButton, Button cancelSecondButton, Button addEqualButton, Button addPerpendicularButton)
        {
            cancelFirstButton.Click += EdgeFirstCancel;
            cancelSecondButton.Click += EdgeSecondCancel;
            addEqualButton.Click += AddEqualRelation;
            addPerpendicularButton.Click += AddPerpendicularRelation;
        }

        private void AddPerpendicularRelation(object sender, EventArgs e)
        {
            if (!CanAddRelation() || !AddRelation(EdgeRelation.Perpendicular))
            {
                errorLabel.Text = "You can't add this relation";
            }
            else
            {
                errorLabel.Text = "";
            }
        }

        private void AddEqualRelation(object sender, EventArgs e)
        {
            if (!CanAddRelation() || !AddRelation(EdgeRelation.EqualLength))
            {
                errorLabel.Text = "You can't add this relation";
            }
            else
            {
                errorLabel.Text = "";
            }
        }

        private bool AddRelation(EdgeRelation relationType)
        {
            if (relatedEdges.Item1.UnderlyingPolygon == null)
                return false;

            var relationInfo = new RelationInfo(relatedEdges.Item1, relatedEdges.Item2, relationType);
            if (!relatedEdges.Item1.UnderlyingPolygon.AddRelation(relationInfo))
                return false;

            return true;
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
            else
                errorLabel.Text = "";

            if (relatedEdges.Item1 == null)
                relatedEdges.Item1 = edge;
            else if (relatedEdges.Item2 == null)
                relatedEdges.Item2 = edge;
        }

        private bool CanAddRelation()
        {
            if (relatedEdges.Item1 == null || relatedEdges.Item2 == null)
                return false;

            return true;
        }

        private void EdgeFirstCancel(object sender, EventArgs e)
        {
            relatedEdges.Item1 = null;
        }

        private void EdgeSecondCancel(object sender, EventArgs e)
        {
            relatedEdges.Item2 = null;
        }
    }
}
