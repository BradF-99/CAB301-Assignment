using CAB301_Assignment.Models;
using System;
using System.Linq;

namespace CAB301_Assignment.ViewModels
{
    class MemberCollection
    {
        private Member[] memberList = new Member[0]; // has a fit if initialised at 0

        public void MemberAdd(string firstName, string lastName, Address address, string phoneNumber, int password)
        {
            try
            {
                Member newMember = new Member(firstName, lastName, address, phoneNumber, password);
                Array.Resize(ref memberList, memberList.Length + 1);
                memberList[^1] = newMember;
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public void MemberAdd(Member member)
        {
            try
            {
                Array.Resize(ref memberList, memberList.Length + 1);
                memberList[^1] = member;
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public Member MemberLogin(string userName, string password)
        {
            try
            {
                Member member = memberList.SingleOrDefault(member => member.UserName == userName);
                if (member == null)
                {
                    throw new System.ArgumentException("Invalid username and/or password. " +
                    "Please check your credentials and try again.");
                }


                if (!Int32.TryParse(password, out int passwordParse))
                    throw new System.ArgumentException("Invalid username and/or password. " +
                        "Please check your credentials and try again.");

                if (member.Password == passwordParse)
                    return member;
                else
                    throw new System.ArgumentException("Invalid username and / or password. " +
                    "Please check your credentials and try again.");
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public Member MemberSearch(string firstName, string lastName)
        {
            string userName = lastName + firstName;
            try
            {
                Member member = memberList.SingleOrDefault(member => member.UserName == userName);
                if(member == null) {
                    throw new System.ArgumentException("No result found. Please make sure your search query is correct.");
                }
                return member;
            }
            catch (Exception e)
            {
                throw e; // throw back to View to create error view
            }
        }
    }
}
