import { useStore } from "@nanostores/react";
import pageStore from "../stores/page_store";

export default function PageSelector() {
  const $page = useStore(pageStore)

  function nextPage() {
    const currentPage = pageStore.get()
    
    pageStore.set(currentPage + 1);
  }
  function prevPage() {
    const currentPage = pageStore.get()
    if(currentPage <= 1)
      return;
    pageStore.set(currentPage - 1);
  }

  return (
    <div className="flex flex-row gap-3 items-center">
        <button
          id="prevPageButton"
          className="p-2 transition hover:bg-slate-500 rounded-lg"
          onClick={prevPage}
        >
          ⬅️
        </button>
        <h1 className="font-bold">{$page}</h1>
        <button
          id="nextPageButton"
          className="p-2 transition hover:bg-slate-500 rounded-lg"
          onClick={nextPage}
        >
          ➡️
        </button>
      </div>
  );
}
