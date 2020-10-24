namespace _14
{
    // 구체적인 처리 내용이 주제가 아니므로 생략
    class PhysicalDistributionBase
    {
        public Baggage Ship(Baggage baggage)
        {
            return baggage;
        }

        public void Receive(Baggage baggage)
        {
            // ...
        }

        public void Transport(PhysicalDistributionBase to, Baggage baggage)
        {
            var shippedBaggage = Ship(baggage);
            to.Receive(shippedBaggage);

            // 운송 기록 같은 것도 필요할 것이다
        }
    }
}
