const observers = new WeakMap();

export const flowgraph = {
    observe: (root, dotnetRef) => {
        if (!root) return;

        const ro = new ResizeObserver(() => {
            // notify C# to recalc
            dotnetRef.invokeMethodAsync("OnResizeAsync");
        });

        ro.observe(root);
        observers.set(root, ro);
    },

    unobserve: (root) => {
        const ro = observers.get(root);
        if (ro) {
            ro.disconnect();
            observers.delete(root);
        }
    },

    getRects: (root, ids) => {
        const rootRect = root.getBoundingClientRect();
        const result = {};

        for (const id of ids) {
            const el = root.querySelector(`[data-node-id="${id}"]`);
            if (!el) continue;

            const r = el.getBoundingClientRect();
            // coords relative to root (important)
            result[id] = {
                x: r.left - rootRect.left,
                y: r.top - rootRect.top,
                w: r.width,
                h: r.height
            };
        }
        return result;
    }
};
