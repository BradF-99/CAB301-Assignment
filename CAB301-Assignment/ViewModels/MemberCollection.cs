using CAB301_Assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CAB301_Assignment.ViewModels
{
    class MemberCollection
    {
        private Member[] memberList;

        public void MemberAdd(string firstName, string lastName, Address address, string phoneNumber, int password)
        {
            try
            {
                int arrLength = memberList.Length;
                Member newMember = new Member(firstName, lastName, address, phoneNumber, password);
                Array.Resize(ref memberList, arrLength + 1);
                memberList[arrLength - 1] = newMember;
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public Member MemberSearch(string firstName, string lastName)
        {
            string userName = lastName + firstName;
            try
            {
                Member result = memberList.SingleOrDefault(member => member.UserName == userName);
                return result;
            }
            catch (Exception e)
            {
                throw e; // throw back to View to create error view
            }
        }
    }
}
