﻿using System;

namespace WisdomLight.ViewModel.Components.Data.Units.Fields
{
    public class DateExpression : TypeLabel, IExpression
    {
        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Value));
            }
        }

        public string Value => Date.ToString();

        public IExpression Clone()
        {
            return new DateExpression
            {
                Name = Name,
                Date = Date,
                Type = Type
            };
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is not IExpression textObj)
                return false;
            else
                return Equals(textObj);
        }

        public bool Equals(IExpression other)
        {
            if (other == null)
                return false;

            return Name == other.Name &&
                Value == other.Value &&
                Type == other.Type;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
