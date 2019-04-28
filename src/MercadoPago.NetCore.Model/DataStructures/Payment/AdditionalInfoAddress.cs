﻿namespace MercadoPago.NetCore.Model.DataStructures.Payment
{
    public struct AdditionalInfoAddress
    {
        #region Properties 
        
        private string _street_name;
        private int _street_number;
        
        private string _zip_code; 
        #endregion

        #region Accessors 
        /// <summary>
        /// Street name
        /// </summary>
        public string StreetName
        {
            get { return _street_name; }
            set { _street_name = value; }
        }
        /// <summary>
        /// Street name
        /// </summary>
        public int StreetNumber
        {
            get { return _street_number; }
            set { _street_number = value; }
        }
        /// <summary>
        /// Zip code
        /// </summary>
        public string ZipCode
        {
            get { return _zip_code; }
            set { _zip_code = value; }
        }


        #endregion
    }
}
