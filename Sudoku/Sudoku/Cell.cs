using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class Cell
    {
        private int m_number;
        private List<int> m_pNumbers;
        private bool m_fixed = false;

        /* CONSTRUCTOR */
        public Cell(int number, bool Fixed) {
            m_number = number;
            m_fixed = Fixed;
        }


        /************************
        * 
        *    SETTERS
        * 
        ************************/
        public void addPossibleNumber(int number) {
            if (!m_pNumbers.Contains(number))
            {
                m_pNumbers.Add(number);
            }
        }
        public void addPossibleNumber(List<int> numbers)
        {
            m_pNumbers = numbers;
        }
        
        public void setNumber(int number)
        {
            m_number = number;
        }

        public void removePossibleNumber(int number)
        {
            m_pNumbers.Remove(number);
        }


        /************************
        * 
        *    GETTERS
        * 
        ************************/
        public int getNumber()
        {
            return m_number;
        }
        public List<int> getPossibleNumbers()
        {
            return m_pNumbers;
        }
        public int getOnlyNumber()
        {
            return m_pNumbers[0];
        }
        public bool isFixed()
        {
            return m_fixed;
        }
    }
}
