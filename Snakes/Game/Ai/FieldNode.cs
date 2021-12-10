using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Snakes.Game.Field;
using Snakes.Tools;

namespace Snakes.Game.Ai
{
    public class FieldNode
    {
        public List<FieldNode> Children
        {
            get { return _children ??= GenerateChildren(); }
        }
        private List<FieldNode> _children;
        public GameField State;
        public Direction Delta;
        public bool Maximizing;
        public int Number;
        public FieldNode Parent;

        public int Score
        {
            get
            {
                _score ??= State.GetPlayer(Number).PossibleMoves - State.GetPlayer(Utilities.Opposite(Number)).PossibleMoves;
                return (int)_score;
            }
        }

        private int? _score;

        public FieldNode(GameField state, int number, FieldNode parent, Direction delta = Direction.None,
            bool maximizing = true)
        {
            State = state;
            Maximizing = maximizing;
            Number = number;
            Parent = parent;
            Delta = delta;
        }

        public List<FieldNode> GenerateChildren()
        {
            List<FieldNode> result = new List<FieldNode>();
            GetStates(result, Maximizing ? Number : Utilities.Opposite(Number));
            return result;
        }

        private void GetStates(List<FieldNode> result, int number)
        {
            for (int i = 1; i < 5; i++)
            {
                if (State.GetPlayer(number).CanMove((Direction)i))
                {
                    var newState = State.Copy();
                    newState.GetPlayer(number).Move((Direction)i);
                    result.Add(new FieldNode(newState, Number,this,(Direction)i, !Maximizing));
                }
            }
        }

        [SuppressMessage("ReSharper.DPA", "DPA0001: Memory allocation issues")]
        public static FieldNode Minimax(FieldNode node,int depth, int alpha, int beta)
        {
            if (depth == 0 || node.State.GameOver) return node;
            if (node.Maximizing)
            {
                FieldNode valueNode = null;
                foreach (var child in node.Children)
                {
                    var current = Minimax(child, depth - 1, alpha, beta);
                    if (valueNode == null || valueNode.Score < current.Score)
                    {
                        valueNode = current;
                    }
                    if (valueNode.Score > beta) break;
                    alpha = Math.Max(alpha, valueNode.Score);
                }
                return valueNode;
            }
            else
            {
                FieldNode valueNode = null;
                foreach (var child in node.Children)
                {
                    var current = Minimax(child, depth - 1, alpha, beta);
                    if (valueNode == null || valueNode.Score > current.Score)
                    {
                        valueNode = current;
                    }
                    if (valueNode.Score < alpha) break;
                    beta = Math.Min(beta, valueNode.Score);
                }
                return valueNode;
            }
        }

    }
}