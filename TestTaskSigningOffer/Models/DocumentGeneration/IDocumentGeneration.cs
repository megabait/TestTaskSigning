namespace TestTaskSigningOffer.Models.DocumentGeneration
{
    public interface IDocumentGeneration
    {
        string CreateDocument(Contract contract, Company company);
    }
}
