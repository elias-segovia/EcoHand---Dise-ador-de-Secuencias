
namespace EcoHand
{
    // Interfaz común para elementos que se pueden seleccionar en DesignerCanvas;
    //utilizado por DesignerItem y Connection

    public interface ISelectable
    {
        bool IsSelected { get; set; }
    }
}
