namespace _15
{
    class TransportService
    {
        public void Transport(PhysicalDistributionBase from, PhysicalDistributionBase to, Baggage baggage)
        {
            var shippedBaggage = from.Ship(baggage);
            to.Receive(shippedBaggage);

            // 운송 기록을 남김
        }
    }
}
