import {HttpResponse} from '@angular/common/http';

// Загрузка файла для экспорта
export function DownloadFile(response: HttpResponse<Blob> | File) {
  if (response instanceof File) {
    download(response.name, response);
  } else {
    const depositionHeader = response.headers.get('content-disposition');
    const fileNameHeader = depositionHeader?.split("''").at(-1);
    if (fileNameHeader && response.body?.size) {
      const fileName = decodeURIComponent(fileNameHeader);
      download(fileName, response.body);
    }
  }
}

function download(fileName: string, file: Blob | File) {
  const a = document.createElement('a');
  const objectUrl = URL.createObjectURL(file);
  a.href = objectUrl;
  a.download = fileName;
  a.click();
  URL.revokeObjectURL(objectUrl);
}
