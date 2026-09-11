mergeInto(LibraryManager.library, {
    DownloadCSV: function (dataPtr, filenamePtr) {

        var data = UTF8ToString(dataPtr);
        var filename = UTF8ToString(filenamePtr);

        var blob = new Blob(
            [data],
            { type: "text/csv;charset=utf-8;" }
        );

        var link = document.createElement("a");

        link.href = URL.createObjectURL(blob);
        link.download = filename;

        document.body.appendChild(link);

        link.click();

        document.body.removeChild(link);

        URL.revokeObjectURL(link.href);
    }
});