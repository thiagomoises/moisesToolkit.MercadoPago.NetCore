﻿namespace MercadoPago.NetCore.Model.DataStructures.Payment
{
    public struct Shipment
    {
        #region Properties 
        private ReceiverAddress? _receiver_address; 
        #endregion

        #region Accessors
        /// <summary>
        /// Buyer's address
        /// </summary>
        public ReceiverAddress? ReceiverAddress
        {
            get { return _receiver_address; }
            set { _receiver_address = value; }
        } 
        #endregion
    }
}
