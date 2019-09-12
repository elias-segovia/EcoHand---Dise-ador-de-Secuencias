using System.Windows;
using System.Windows.Controls;

namespace EcoHand
{
    // Implementa ItemsControl para ToolboxItems    
    public class Toolbox : ItemsControl
    {

        // Define las propiedades ItemHeight y ItemWidth del WrapPanel utilizado para Toolbox
        public Size ItemSize
        {
            get { return itemSize; }
            set { itemSize = value; }
        }
        private Size itemSize = new Size(50, 50);

        // Crea o identifica el elemento que se usa para mostrar el elemento dado.
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ToolboxItem();
        }


        // Determina si el elemento especificado es (o es elegible para ser) su propio contenedor.
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return (item is ToolboxItem);
        }
    }
}
