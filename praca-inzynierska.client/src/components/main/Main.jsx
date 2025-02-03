import { useState } from "react";
import Image from "../imageFigure/Image";
import Prompt from "../prompt/Prompt";
import styles from "./Main.module.css";

function Main() {
  const [query, setQuery] = useState("");
  const [imageBeforeURL, setImageBeforeURL] = useState(null);
  const [imageBefore, setImageBefore] = useState(null);
  const [imageAfterURL, setImageAfterURL] = useState(null);

  function onImageBeforeChange(e) {
    if (e.target.files && e.target.files[0]) {
      setImageBeforeURL(URL.createObjectURL(e.target.files[0]));
      setImageBefore(e.target.files[0]);
    }
  }

  function onSubmit(e) {
    e.preventDefault();
    if (query.length <= 0 || !imageBefore) return;

    async function requestProcessImage() {
      try {
        const formData = new FormData();
        formData.append("text", query);
        formData.append("File", imageBefore);

        const res = await fetch("/api/Main/process", {
          method: "POST",
          body: formData,
        });

        if (!res.ok) {
          throw new Error(`HTTP error! Status: ${res.status}`);
        }

        const data = await res.blob();
        setImageAfterURL(URL.createObjectURL(data));
      } catch (err) {
        console.error(err);
      }
    }
    requestProcessImage();
  }

  return (
    <main className={styles.main}>
      <Prompt
        query={query}
        setQuery={setQuery}
        onChange={onImageBeforeChange}
        onSubmit={onSubmit}
      />

      <div className={styles.images}>
        {imageBefore && (
          <>
            <Image
              title="Obraz - Przed"
              imageSrc={imageBeforeURL}
              imageAlt="Image - before"
            />
            {imageAfterURL ? (
              <Image
                title="Obraz - Po"
                imageSrc={imageAfterURL}
                imageAlt="Image - after"
              />
            ) : (
              <div className={styles.notify}>&nbsp;</div>
            )}
          </>
        )}
      </div>
    </main>
  );
}

export default Main;
