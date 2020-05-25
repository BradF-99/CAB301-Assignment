using CAB301_Assignment.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CAB301_Assignment.ViewModels
{
    class MovieCollection
    {
        public BSTree<Movie> Tree; 
        public MovieCollection()
        {
            this.Tree = new BSTree<Movie>();
        }

        public class Node<Movie>
        {
            public Node<Movie> Left;
            public Node<Movie> Right;
            public Node<Movie> Parent;
            public Models.Movie Data;

            public Node(Models.Movie movie)
            {
                this.Left = null;
                this.Right = null;
                this.Data = movie;
            }
        }

        public class BSTree<Movie>
        {
            public Node<Movie> Root;

            public BSTree()
            {
                this.Root = null;
            }

            public Models.Movie Search(string query, Node<Movie> node)
            {
                if (node == null)
                    node = this.Root;
               
                if (node == null)
                    throw new ArgumentException("Movie not found. Please check your query and try again.");
                // hash the titles to check which is larger
                // if equal we have the correct node
                else if (query.GetHashCode() == node.Data.Title.GetHashCode()) 
                    return node.Data;
                else if (query.GetHashCode() < node.Data.Title.GetHashCode())
                    return this.Search(query, node.Left);
                else
                    return this.Search(query, node.Right);
            }

            // used for editing existing data in nodes
            public Node<Movie> NodeSearch(string query, Node<Movie> node)
            {
                if (node == null)
                    node = this.Root;

                if (node == null)
                    throw new ArgumentException("Movie not found. Please check your query and try again.");
                else if (query.GetHashCode() == node.Data.Title.GetHashCode())
                    return node;
                else if (query.GetHashCode() < node.Data.Title.GetHashCode())
                    return this.NodeSearch(query, node.Left);
                else
                    return this.NodeSearch(query, node.Right);
            }

            public void BorrowMovie(Models.Movie movie)
            {
                Node<Movie> movieNode = this.NodeSearch(movie.Title,null);
                if(movieNode.Data.CopiesAvailable > 0)
                {
                    movieNode.Data.CopiesAvailable--;
                    movieNode.Data.AmountBorrowed++;
                }
                else
                {
                    throw new ArgumentException("There are no copies of this movie left to borrow.");
                }
            }

            public void Insert(Models.Movie movie)
            {
                Node<Movie> newNode = new Node<Movie>(movie);
                Node<Movie> current, tempParent;

                if (this.Root == null)
                {
                    //First node insertion
                    this.Root = newNode;
                }
                else
                { 
                    current = this.Root;
                    while (true)
                    {
                        tempParent = current;
                        if (newNode.Data.GetHashCode() < current.Data.GetHashCode())
                        {
                            current = current.Left;
                            if (current == null)
                            {
                                tempParent.Left = newNode;
                                newNode.Parent = tempParent;
                                return;
                            }
                        }
                        else
                        {
                            current = current.Right;
                            if (current == null)
                            {
                                tempParent.Right = newNode;
                                newNode.Parent = tempParent;
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}
