import { useState } from "react";
import Image from "../imageFigure/Image";
import Prompt from "../prompt/Prompt";
import styles from "./Main.module.css";

function Main() {
    const [query, setQuery] = useState("");
    const [imageBeforeURL, setImageBeforeURL] = useState(null);
    const [imageBefore, setImageBefore] = useState(null)

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
                const res = await fetch("Main/process", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify({ text: query, File: imageBefore }),
                });
                const data = await res.json();
                console.log(data)
                setImageAfterURL(URL.createObjectURL(data))
            } catch (err) {
                console.log(err)
            }
        }
        requestProcessImage();
    }

  return (
      <main className={styles.main}>
          <Prompt query={query} setQuery={setQuery} onChange={onImageBeforeChange} onSubmit={onSubmit} />

          <div className={styles.images}>
              {imageBefore && <>
                  <Image title="Image - Before" imageSrc={imageBeforeURL} imageAlt="Image - before" />
                  {imageAfterURL ? < Image title="Image - After" imageSrc={imageAfterURL} imageAlt="Image - after" /> : <div className={styles.notify}>&nbsp;</div>}
                  </>
              }
      </div>
    </main>
  );
}

export default Main;
