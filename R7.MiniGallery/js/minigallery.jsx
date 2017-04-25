class MiniGalleryImage extends React.Component {
    render() {
        return (
            <span className="MG_Item">
                <a href={this.props.EditUrl}>[edit]</a>
                <a href={this.props.Src} target="_blank" title={this.props.Title} className="MG_Link">
                    <img src={this.props.Src} alt={this.props.Alt} className="MG_Image" />
                </a>
            </span>
        );
    }
}

class BlueimpGallery extends React.Component {
    render() {
        return (
            <div>
                <div className="blueimp-gallery blueimp-gallery-controls">
                    <div className="slides"></div>
                    <h3 className="title"></h3>
                    <a className="prev">&lt;</a>
                    <a className="next">&gt;</a>
                    <a className="close">&times;</a>
                    <a className="play-pause"></a>
                    <ol className="indicator"></ol>
                </div>
                <div className="MG_List">
                    {this.props.Images.map((img) => <MiniGalleryImage Src={img.ImageSrc} Alt={img.Alt} Title={img.Title} EditUrl={img.EditUrl} />)}
                </div>
            </div>
        );
    }
}

(function ($, window, document) {
    $(() => {
        $(".minigallery-inner").each ((i, m) => {
            var moduleId = $(m).data ("module-id");
            ReactDOM.render(
                <BlueimpGallery ModuleId={moduleId} Images={$(m).data ("images")} />, m
            );
            var container = $(m).find(".blueimp-gallery").first();
            var links = $(m).find("a.MG_Link");
            $("a.MG_Link").click ((event) => {
                var target = event.target || event.srcElement,
                link = target.src ? target.parentNode : target,
                options = {
                    index: link,
                    event: event,
                    container: container
                };
                blueimp.Gallery(links, options);
            });
        });
    });
}) (jQuery, window, document);