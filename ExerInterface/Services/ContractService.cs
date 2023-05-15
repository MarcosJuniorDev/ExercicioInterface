using ExerInterface.Entities;

namespace ExerInterface.Services
{
    internal class ContractService
    {
        
        public IOnlinePaymentService OnlinePaymentService { get; set; }

        public ContractService(IOnlinePaymentService onlinePaymentService)
        {
            OnlinePaymentService = onlinePaymentService;
        }

        public void ProcessContract(Contract contract, int months)
        {
            DateTime dataParcela;
            double valorParcela = contract.TotalValue / months;
            double valorTotalParcela;
            for (int i = 1; i <= months; i++)
            {
                dataParcela = contract.Date.AddMonths(i);
                valorTotalParcela = valorParcela + OnlinePaymentService.Interest(valorParcela, i);
                valorTotalParcela += OnlinePaymentService.PaymentFee(valorTotalParcela);
                Installment parcela = new Installment(dataParcela, valorTotalParcela);
                contract.AddInstallment(new Installment(dataParcela, valorTotalParcela));

            }

        }




    }
}
