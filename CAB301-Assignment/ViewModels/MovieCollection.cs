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
                try
                {
                    if (node == null) // check twice
                        return null;
                    // if comparison is lower go left
                    if (query.ToLower().CompareTo(node.Data.Title.ToLower()) == -1)
                        return this.NodeSearch(query, node.Left);
                    // if comparison is higher go right
                    else if (query.ToLower().CompareTo(node.Data.Title.ToLower()) == 1)
                        return this.NodeSearch(query, node.Right);
                    // we have the correct node
                    else
                        return node;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            public void BorrowMovie(Models.Movie movie)
            {
                Node<Movie> movieNode = this.NodeSearch(movie.Title, this.Root);
                if (movieNode.Data.CopiesAvailable > 0)
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
                Node<Movie> movieNode = this.NodeSearch(movie.Title, this.Root);
                movieNode.Data.CopiesAvailable++;
            }

            public void Insert(Models.Movie movie)
            {
                Node<Movie> newNode = new Node<Movie>(movie);
                Node<Movie> currentNode, tempParentNode;

                if (this.Root == null) // if the tree is empty insert at root
                {
                    this.Root = newNode;
                }
                else
                {
                    currentNode = this.Root;
                    while (true)
                    {
                        tempParentNode = currentNode;
                        if (newNode.Data.Title.ToLower().CompareTo(currentNode.Data.Title.ToLower()) == -1)
                        {
                            currentNode = currentNode.Left;
                            if (currentNode == null)
                            {
                                tempParentNode.Left = newNode;
                                newNode.Parent = tempParentNode;
                                return;
                            }
                        }
                        else
                        {
                            currentNode = currentNode.Right;
                            if (currentNode == null)
                            {
                                tempParentNode.Right = newNode;
                                newNode.Parent = tempParentNode;
                                return;
                            }
                        }
                    }
                }
            }

            public Node<Movie> MinimumKey(Node<Movie> node)
            {
                while(node.Left != null)
                {
                    node = node.Left;
                }
                return node;
            }

            public void Remove(Models.Movie movie)
            {
                Node<Movie> deleteNode = this.NodeSearch(movie.Title, this.Root);
                Node<Movie> deleteNodeParent, deleteNodeChild; // used for temporarily storing parent and child nodes

                if (deleteNode != null) // make sure we aren't deleting a node that doesn't exist
                {
                    // first check if it's a leaf node
                    if (deleteNode.Left == null && deleteNode.Right == null)
                    {
                        deleteNodeParent = deleteNode.Parent; // assign parent to variable
                        if (deleteNode == deleteNodeParent.Left) // check if deleted node is on left or right
                            deleteNodeParent.Left = null; // delete it from the parent
                        else // will be on the right
                            deleteNodeParent.Right = null;
                    }
                    // if true it has a child left or right but not at both
                    else if (deleteNode.Left == null ^ deleteNode.Right == null)
                    {
                        deleteNodeParent = deleteNode.Parent; // get the parent to store the children in

                        if (deleteNode.Left == null) // check if child is on left or right
                            deleteNodeChild = deleteNode.Right; // get the right side and store it
                        else
                            deleteNodeChild = deleteNode.Left; // get left side and store it

                        if (deleteNode == deleteNodeParent.Left) // if deleted node is on the left
                            deleteNodeParent.Left = deleteNodeChild; // store the child node in its place on the left
                        else
                            deleteNodeParent.Right = deleteNodeChild; // or on the right

                    }
                    // it has both left and right children
                    else
                    {
                        if(deleteNode.Left.Right == null) // edge case where right tree of left child is empty
                        {
                            deleteNode.Data = deleteNode.Left.Data;
                            deleteNode.Left = deleteNode.Left.Left;
                        }
                        else
                        {
                            Node<Movie> deleteNodeLeft = deleteNode.Left; // get left node of deleted
                            while(deleteNodeLeft.Right != null)
                            {
                                deleteNode = deleteNodeLeft;
                                deleteNodeLeft = deleteNodeLeft.Right;
                            }
                            deleteNode.Data = deleteNodeLeft.Data;
                            deleteNode.Right = deleteNodeLeft.Left;
                        }


                    }
                }
                else
                {
                    throw new ArgumentException("Movie not found in database.");
                }
            }

            public List<Models.Movie> Traverse()
            {
                List<Models.Movie> movieList = new List<Models.Movie>();
                InOrderTraverse(movieList, this.Root);
                return movieList;
            }

            public void InOrderTraverse(List<Models.Movie> list, Node<Movie> root)
            {
                if(root != null)
                {
                    InOrderTraverse(list, root.Left);
                    list.Add(root.Data);
                    InOrderTraverse(list, root.Right);
                }
            }
        }
    }
}
