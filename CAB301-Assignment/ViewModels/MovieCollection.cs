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

            public Models.Movie Search(string query)
            {
                try
                {
                    Models.Movie result = Search(query, this.Root);
                    if (result == null)
                         throw new ArgumentException("Movie not found. Please check your query and try again.");
                    return result;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            public Models.Movie Search(string query, Node<Movie> node)
            {
                try
                {
                    if (node == null) // check twice
                        return null;
                    // if comparison is lower go left
                    if (query.ToLower().CompareTo(node.Data.Title.ToLower()) == -1)
                        return this.Search(query, node.Left);
                    // if comparison is higher go right
                    else if (query.ToLower().CompareTo(node.Data.Title.ToLower()) == 1)
                        return this.Search(query, node.Right);
                    // we have the correct node
                    else
                        return node.Data;
                }
                catch (Exception e)
                {
                    throw e;
                }
                
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

            public void ReturnMovie(Models.Movie movie)
            {
                Node<Movie> movieNode = this.NodeSearch(movie.Title, null);

                movieNode.Data.CopiesAvailable++;
                movieNode.Data.AmountBorrowed--;
               
            }

            public void Insert(Models.Movie movie)
            {
                Node<Movie> newNode = new Node<Movie>(movie);
                Node<Movie> current, tempParentNode;

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
                        tempParentNode = current;
                        //query.ToLower().CompareTo(node.Data.Title.ToLower()) == -1
                        //newNode.Data.GetHashCode() < current.Data.GetHashCode()
                        if (newNode.Data.Title.ToLower().CompareTo(current.Data.Title.ToLower()) == -1)
                        {
                            current = current.Left;
                            if (current == null)
                            {
                                tempParentNode.Left = newNode;
                                newNode.Parent = tempParentNode;
                                return;
                            }
                        }
                        else
                        {
                            current = current.Right;
                            if (current == null)
                            {
                                tempParentNode.Right = newNode;
                                newNode.Parent = tempParentNode;
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}
